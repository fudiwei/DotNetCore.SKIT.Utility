using System;
using System.Text;
using System.Security.Cryptography;
using Xunit;

namespace STEP.Utility.UnitTests
{
#if !NETFRAMEWORK
    public class RsaUtilUnitTests
    {
        const string STR_EN = "hello, world.";
        const string STR_CH = "ÄãºÃ£¬ÊÀ½ç¡£"; 
        const string RSA_PUBLIC_KEY_1024 = "-----BEGIN PUBLIC KEY-----"
                                    + "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCh5Nk2GLiyQFMIU+h3OEA4UeFb"
                                    + "u3dCH5sjd/sLTxxvwjXq7JLqJbt2rCIdzpAXOi4jL+FRGQnHaxUlHUBZsojnCcHv"
                                    + "hrz2knV6rXNogt0emL7f7ZMRo8IsQGV8mlKIC9xLnlOQQdRNUssmrROrCG99wpTR"
                                    + "RNZjOmLvkcoXdeuaCQIDAQAB"
                                    + "-----END PUBLIC KEY-----";
        const string RSA_PRIVATE_KEY_1024 = "-----BEGIN RSA PRIVATE KEY-----"
                                    + "MIICWwIBAAKBgQCh5Nk2GLiyQFMIU+h3OEA4UeFbu3dCH5sjd/sLTxxvwjXq7JLq"
                                    + "Jbt2rCIdzpAXOi4jL+FRGQnHaxUlHUBZsojnCcHvhrz2knV6rXNogt0emL7f7ZMR"
                                    + "o8IsQGV8mlKIC9xLnlOQQdRNUssmrROrCG99wpTRRNZjOmLvkcoXdeuaCQIDAQAB"
                                    + "AoGAUTcJ1H6QYTOts9bMHsrERLymzir8R9qtLBzrfp/gRxxpigHGLdph8cWmk8dl"
                                    + "N5HDRXmmkdV6t2S7xdOnzZen31lcWe0bIzg0SrFiUEOtg3Lwxzw2Pz0dKwg4ZUoo"
                                    + "GKpcIU6kEpbC2UkjBV4+2E6P1DXuhdgTyHoUA3ycxOdjCAUCQQCyjTzGPXFoHq5T"
                                    + "miJyVd4VXNyCXGU0ZuQayt6nPN8Gd5CcEb2S4kggzPXQcd90FO0kHfZV6+PGTrc2"
                                    + "ZUuz5uwPAkEA6B3lmEmiZsJS/decLzWR0T1CXaFGwTjBQbHXJ0RziAfkuy+VwSmh"
                                    + "vrW/ipk5xbREr5rKx3jVI2PzVOvLw7NgZwJAbUsvDFnH9WfyZZJPy5TsID97awCL"
                                    + "oovozM2phM0p55eAmUfyttp0ND/BqBpMIY49qoH8q5N9FYJRe6Z9tF2B2QJAQBEo"
                                    + "cw039xcB4zCk2l713YQEEmXWarSomuJkWWFKZiyPlJ8Ava0pCMOPl8jNKmWkY7fc"
                                    + "6ovOgJMw8aqXtm+HVwJAerJeUEDez2djG5pIF6aCV0bP3fhQUq8OQCgGF5Qzo9Cn"
                                    + "qvYreGpYKPJGVixAsEPCiLzJRhy1XfFona6VRXIIxw=="
                                    + "-----END RSA PRIVATE KEY-----";
        const string RSA_PUBLIC_KEY_2048 = "-----BEGIN PUBLIC KEY-----"
                                    + "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwd+PToipWA/p0eyjBJuL"
                                    + "es2QMBfB/Bpka02mchPLyQ1NOtDtgGsl3P3WRSDFfeW9vglfQtsgVZQmR5aT2iT4"
                                    + "NBST2KuwpO92OpmrDu0hDJb/pPIf7SiaSryq/JyTy6RBmXUG1mQGAAbLkZLRf1RK"
                                    + "aftLRdwquqs0fNBbvcWNZb/cMU8OkuPIpDBeMtsylwkt5LY5nQJsEb3kBVbuIVIq"
                                    + "gJhByjYRu2VKSc0xadQm3qmPLdU3UFx8A0y19lq6dYI4HYVnbVmkjZtU3n0d/S5R"
                                    + "ZEl4ctkg4wV/RrYZjS6nLB3PU0SuxbnNXBBz7DA3yxEYRMGVEBQy6gPFx2D3mzzb"
                                    + "kwIDAQAB"
                                    + "-----END PUBLIC KEY-----";
        const string RSA_PRIVATE_KEY_2048 = "-----BEGIN RSA PRIVATE KEY-----"
                                    + "MIIEpQIBAAKCAQEAwd+PToipWA/p0eyjBJuLes2QMBfB/Bpka02mchPLyQ1NOtDt"
                                    + "gGsl3P3WRSDFfeW9vglfQtsgVZQmR5aT2iT4NBST2KuwpO92OpmrDu0hDJb/pPIf"
                                    + "7SiaSryq/JyTy6RBmXUG1mQGAAbLkZLRf1RKaftLRdwquqs0fNBbvcWNZb/cMU8O"
                                    + "kuPIpDBeMtsylwkt5LY5nQJsEb3kBVbuIVIqgJhByjYRu2VKSc0xadQm3qmPLdU3"
                                    + "UFx8A0y19lq6dYI4HYVnbVmkjZtU3n0d/S5RZEl4ctkg4wV/RrYZjS6nLB3PU0Su"
                                    + "xbnNXBBz7DA3yxEYRMGVEBQy6gPFx2D3mzzbkwIDAQABAoIBAQCYZuNCew+UGD5Y"
                                    + "NUsYziVhDcLw61wkj6Ks70eOmZ0ymPBC8gYhUxlalXggs1hMVZNIlhl6dsL+Qw2s"
                                    + "bOQhMbqjRiHKy3x6y3sHKdFcVHAMc47W3TbXuXlAkvtexL8x8BdZSLNtSQemcbEI"
                                    + "6H8jNuGgWlibvC0ivH7wNuJHVcqHVmLBgXi6dmm9bMRup8aMp+ROv4k2CULcLgsX"
                                    + "D1o5GWF4LoRKcVaHP9NMNSsK471w7P/5CN/X9JJFn4io5QRZDZlbNuUPGPLbkw7c"
                                    + "mbDUl5jEQC1VfuaakIg4sJFu33ZMxv9rN0jyybgcxkBzv5VNuSNg9urlI8u4ddZN"
                                    + "rVp1rrwxAoGBAN8iS86o/iL5ryUJQlHaMdWGGhnuA3lxD6AMt1Y2n8n4z/lUvxgh"
                                    + "QFdhlQWw8eVO+3fqc5gf5ROS6IM1/Vi4x8Hbij3OiIvSz+8vaPeXxKWnGR4YwdxE"
                                    + "WAPO9pewfYiOJA5RjYNltzvaujdCNlvlKVjRIkDhZbdBS/xjwlYa2S/NAoGBAN5t"
                                    + "7pjFqAx35YVkVNMUkGu++2mB2A4KSk9a3vkuMIfbuyRthqIF7FPqGG6B0mck/ZAS"
                                    + "nCnxgWKGWJQ5UK83NTo3cf4/8ivgzISfmiF0lh41dQnVaeyW2cDe36WkhCxC2uUF"
                                    + "niTgAv3CqMnB9PbQeeUHGtozHtxbcKoiu/9u1RjfAoGBAIbGb2WPO5mimMDVG+LW"
                                    + "2VzwmBlrY1vaB6cTpzWC3vceu3gNUTNg+j0Nava6DxIDp+6hhVqwgSxWguymErWh"
                                    + "Pr8APTrh4iYampANYeiTGis4h/pe19GU0ljSjK3I47o0qOChL8nbCVc04V95Nd5B"
                                    + "x7ym7Xqk6kxLO3tiQkLCCsdNAoGBALeB8Ohfog5vWJAdv5HKFICgNyHLuymSOc6Q"
                                    + "hQcFoYpksVgTeJDx3BE7QF7jgmgQb5XOlMJR+lIDzs6zHqsAHEzkc4q0zSKAO5tr"
                                    + "ZakWW8eeiOnNBa/ooMxr1A3/1gACRD/Qy7FWk4EyeTjDaUu7oeVfYDsHE/3u/tuO"
                                    + "/pV1ph/3AoGANqGpRZW8xDiqdukh5LKAak0DymSyayVZhvtW0EUQ+cAcENF04CbH"
                                    + "JzBcPIf/aWjhEoboHWuLJadCKiN6HackNktN9Ixo44msdbtCw+HutNBKiGPhT62L"
                                    + "dofmElg6VRpoCXH0lJgBLONw2QyELi3mVUfdKu+RqEK2qb3H4czPtPs="
                                    + "-----END RSA PRIVATE KEY-----";

        [Fact]
        public void EncryptAndDecryptTest()
        {
            string cipher;

            cipher = RsaUtil.Encrypt(STR_EN, RSA_PUBLIC_KEY_1024);
            Assert.NotEmpty(cipher);
            Assert.Equal(STR_EN, RsaUtil.Decrypt(cipher, RSA_PRIVATE_KEY_1024));
            Assert.Equal(STR_EN, RsaUtil.Decrypt("QccbdMDHAZflCKBDrKVH/YH4Nx6XiSNUAbscDppOsIheIM1kTa3+GDnXPjM3l7xXOOHpN3f2JkjX/dfUd5SPdNqwESepO28GRzVq4j4QIg7sKQ2a1NV4jy1Zfz+ssvvyJFDk9ycnb7d0pRS9UwqO/pbgP6YS+2MQrY2r4n7gJEA=", RSA_PRIVATE_KEY_1024));

            cipher = RsaUtil.Encrypt(STR_CH, RSA_PUBLIC_KEY_1024);
            Assert.NotEmpty(cipher);
            Assert.Equal(STR_CH, RsaUtil.Decrypt(cipher, RSA_PRIVATE_KEY_1024));
            Assert.Equal(STR_CH, RsaUtil.Decrypt("OcLlLFAlMCyrgeOsqUaWIsYmBWi5EyWTU1ZnS94gkxYOGdVdkmbmJOB6/sGOfjWmhqg/5eK7VHGa4kpJAZgw5QoDglLiNlKZjCSS7BcliuZEFhhlln6h6UKYQECflgcm73OtbozgOpFG7oLhiaer1ONFMO6az0o4WSp6qBcqTck=", RSA_PRIVATE_KEY_1024));

            cipher = RsaUtil.Encrypt(STR_EN, RSA_PUBLIC_KEY_2048);
            Assert.NotEmpty(cipher);
            Assert.Equal(STR_EN, RsaUtil.Decrypt(cipher, RSA_PRIVATE_KEY_2048));

            cipher = RsaUtil.Encrypt(STR_CH, RSA_PUBLIC_KEY_2048);
            Assert.NotEmpty(cipher);
            Assert.Equal(STR_CH, RsaUtil.Decrypt(cipher, RSA_PRIVATE_KEY_2048));
        }

        [Fact]
        public void OversizeTest()
        {
            /**
             * REF: https://github.com/stulzq/DotnetCore.RSA/issues/2
             *      https://www.cnblogs.com/CreateMyself/p/9853736.html
             */
            string data = CaptchaUtil.GetCaptcha(4096);
            string cipher = RsaUtil.Encrypt(data, RSA_PUBLIC_KEY_1024);
            string sign = RsaUtil.Sign(data, RSA_PRIVATE_KEY_1024);
            Assert.NotEmpty(cipher);
            Assert.NotEmpty(sign);
            Assert.Equal(data, RsaUtil.Decrypt(cipher, RSA_PRIVATE_KEY_1024));
            Assert.True(RsaUtil.Verify(data, sign, RSA_PUBLIC_KEY_1024));
        }

        [Fact]
        public void SignAndVerifyTest()
        {
            string sign;

            sign = RsaUtil.Sign(STR_EN, RSA_PRIVATE_KEY_1024);
            Assert.NotEmpty(sign);
            Assert.True(RsaUtil.Verify(STR_EN, sign, RSA_PUBLIC_KEY_1024));

            sign = RsaUtil.Sign(STR_CH, RSA_PRIVATE_KEY_1024);
            Assert.NotEmpty(sign);
            Assert.True(RsaUtil.Verify(STR_CH, sign, RSA_PUBLIC_KEY_1024));

            sign = RsaUtil.Sign(STR_EN, RSA_PRIVATE_KEY_2048);
            Assert.NotEmpty(sign);
            Assert.True(RsaUtil.Verify(STR_EN, sign, RSA_PUBLIC_KEY_2048));

            sign = RsaUtil.Sign(STR_CH, RSA_PRIVATE_KEY_2048);
            Assert.NotEmpty(sign);
            Assert.True(RsaUtil.Verify(STR_CH, sign, RSA_PUBLIC_KEY_2048));
        }
    }
#endif
}
