using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Logging;
using IdentityServer3.DocumentDb.Repositories;
using IdentityServer3.DocumentDb.Repositories.Impl;

namespace IdentityServer3.DocumentDb
{
    public class TokenCleanup
    {
        private readonly static ILog Logger = LogProvider.GetCurrentClassLogger();

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthorizationCodeRepository _authorizationCodeRepository;
        private readonly ITokenHandleRepository _tokenHandleRepository;
        private readonly TimeSpan _interval;

        private CancellationTokenSource _source;

        public TokenCleanup(DocumentDbServiceOptions options, int interval = 60)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (interval < 1) throw new ArgumentException("interval must be more than 1 second");

            var connectionSettings = options.ToConnectionSettings();
            this._interval = TimeSpan.FromSeconds(interval);

            _refreshTokenRepository = new RefreshTokenRepository(connectionSettings);
            _authorizationCodeRepository = new AuthorizationCodeRepository(connectionSettings);
            _tokenHandleRepository = new TokenHandleRepository(connectionSettings);
        }

        public void Start()
        {
            if (_source != null) throw new InvalidOperationException("Already started. Call Stop first.");

            _source = new CancellationTokenSource();
            Task.Factory.StartNew(() => Start(_source.Token));
        }

        public void Stop()
        {
            if (_source == null) throw new InvalidOperationException("Not started. Call Start first.");

            _source.Cancel();
            _source = null;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Logger.Info("CancellationRequested");
                    break;
                }

                try
                {
                    await Task.Delay(_interval, cancellationToken);
                }
                catch
                {
                    Logger.Info("Task.Delay exception. exiting.");
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    Logger.Info("CancellationRequested");
                    break;
                }

                await ClearTokens();
            }
        }

        private async Task ClearTokensFromRepository<TToken>(ITokenRepository<TToken> repo, DateTimeOffset expiryDate) where TToken : TokenDocument
        {
            var tooOld = await repo.GetExpired(expiryDate);
            foreach (var item in tooOld)
                await repo.RemoveAsync(item.Key);
        }

        private async Task ClearTokens()
        {
            try
            {
                var expiry = DateTimeOffset.UtcNow;
                Logger.Info("Clearing tokens");
                await ClearTokensFromRepository(_tokenHandleRepository, expiry);
                await ClearTokensFromRepository(_refreshTokenRepository, expiry);
                await ClearTokensFromRepository(_authorizationCodeRepository, expiry);
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Exception cleanring tokens", ex);
            }
        }
    }
}
