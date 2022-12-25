using FingerprintBuilder;
using SignalRApp.Entities;
using System;
using System.Security.Cryptography;

namespace SignalRApp.Extensions
{
    /// <summary>
    /// Расширение для работы с хешированием
    /// </summary>
    public static class HashExtension
    {
        private static readonly Func<UserEntity, byte[]> _fingerprintOriginal = FingerprintBuilder<UserEntity>
            .Create(SHA1.Create().ComputeHash)
            .For(p => p.Login)
            .For(p => p.EmailPrimary)
            .For(p => p.FirstName)
            .For(p => p.LastName)
            .For(p => p.JpegPhoto)
            .Build();

        /// <summary>
        /// Генерация хеша для UserEntity
        /// </summary>
        /// <param name="state">Сущность UserEntity для расчета хеша</param>
        /// <returns>Хеш для сущности UserEntity</returns>
        public static string GenerateStateHash(this UserEntity state)
        {
            return _fingerprintOriginal(state).ToLowerHexString();
        }
    }
}
