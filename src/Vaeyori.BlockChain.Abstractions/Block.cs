/*
*    Copyright (C) 2021 Joshua "ysovuka" Thompson
*
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Affero General Public License as published
*    by the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU Affero General Public License for more details.
*
*    You should have received a copy of the GNU Affero General Public License
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace Vaeyori.BlockChain.Abstractions
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public abstract record Block : IBlock
    {
        public Block(DateTimeOffset when, string previousHash)
        {
            When = when;
            PreviousHash = previousHash;
        }

        protected void Initialize()
        {
            Hash = CalculateHash();
        }

        public DateTimeOffset When { get; init; }
        public string PreviousHash { get; init; }

        [JsonIgnore]
        public string Hash { get; private set; }

        public string CalculateHash()
        {
            using var sha512 = SHA512.Create();
            var serializedString = JsonSerializer.Serialize<object>(this);
            byte[] inputBytes = Encoding.UTF8.GetBytes(serializedString);
            byte[] outputBytes = sha512.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }
    }
}
