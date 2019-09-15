using Xunit;

namespace STEP.Utility.UnitTests
{
    public class ShaUtilUnitTests
    {
        const string STR_EN = "hello, world.";
        const string STR_CH = "你好，世界。";
        const string SECRET = "mysecret";

        [Fact]
        public void Sha1Test()
        {
            Assert.Equal("a1a6bb411bac18fc1d0c88eba5a841d0498ea6cf", ShaUtil.SHA1Compute(STR_EN), ignoreCase: true);
            Assert.Equal("6eac95ceea2ea925d82c05ccaecc2554d24e602b", ShaUtil.HMacSHA1Compute(STR_EN, SECRET), ignoreCase: true);

            Assert.Equal("4286f5952ce0b96ea3d4203ea1ae5c0d4a76b420", ShaUtil.SHA1Compute(STR_CH), ignoreCase: true);
            Assert.Equal("7f16927bdb67301586906df2ca9e07c0919eb12f", ShaUtil.HMacSHA1Compute(STR_CH, SECRET), ignoreCase: true);
        }

        [Fact]
        public void Sha256Test()
        {
            Assert.Equal("df4f2d5707fd85996a13aab607eb3f6b19d0e99d97281d5eae68a6c16317a329", ShaUtil.SHA256Compute(STR_EN), ignoreCase: true);
            Assert.Equal("4764056a43528aaa04aaedc7740bd450673836291488a802e5dcfdd869c29218", ShaUtil.HMacSHA256Compute(STR_EN, SECRET), ignoreCase: true);

            Assert.Equal("6ac5565122a5e33aa2289d893a24f2aafe9ab98a42f5128e736dbce6c6e6de8c", ShaUtil.SHA256Compute(STR_CH), ignoreCase: true);
            Assert.Equal("3d0a634e69bf1279c064c90bf1e75cdae5a0c60c58e7ba75f0dff8f7008ce6e6", ShaUtil.HMacSHA256Compute(STR_CH, SECRET), ignoreCase: true);
        }

        [Fact]
        public void Sha384Test()
        {
            /**
             * 注意：
             * 很多在线工具上的 HMAC-SHA384 结果是错误的，因为其使用的是旧版本 crypto.js（REF: https://code.google.com/archive/p/crypto-js/issues/84）。
             * 建议使用 https://www.freeformatter.com/hmac-generator.html#ad-output 做验证。
             */

            Assert.Equal("657b5174f2d0a9bafec4c447ab9dc32022714142dd9946f68591581d9595b80874e79535834e7e84e55a54da66fd9e0c", ShaUtil.SHA384Compute(STR_EN), ignoreCase: true);
            Assert.Equal("1cdf8bb3cf60f1fd2720a93bdb54ff0e125f1d81c226d88f68c71b3665b331658da3a0f99c94613ad56bc440933ca437", ShaUtil.HMacSHA384Compute(STR_EN, SECRET), ignoreCase: true);

            Assert.Equal("f2cd88016b3365d02f6d01818590815919accb2a203c19f505b4521b6fd754bb39e9b19a7c05f91fe7f859a8efdd7b1d", ShaUtil.SHA384Compute(STR_CH), ignoreCase: true);
            Assert.Equal("5d228abf802cdac2b81a46d369a6f9604258c4454be2e4d9866322f7113117cf2decf7acd9e8c8fc55bbf147583885a2", ShaUtil.HMacSHA384Compute(STR_CH, SECRET), ignoreCase: true);
        }

        [Fact]
        public void Sha512Test()
        {
            Assert.Equal("d02b25c6e56150b84089e388fc798437fb94fb0992d8b1566ec64da4bc14ded263c6046058734b302215636db46c05b4b9778692a56c0ed847b65123d74392b3", ShaUtil.SHA512Compute(STR_EN), ignoreCase: true);
            Assert.Equal("256f172697363907489bf3c4e189a1ed9d8cd69f18c46b14992b6106343344c318e486543b05654de7d0a6046eb73c3acc802ef69b3a2dfa979d895d685d76be", ShaUtil.HMacSHA512Compute(STR_EN, SECRET), ignoreCase: true);

            Assert.Equal("2c3cc4eca7aaf5063177b9390cffa511e6ab5345d03e38b50fc4dd09a46158dc8c749d45dac8ec0766d1a139705c78bd7a6ca98f3eee3e83715fa306c8e49dee", ShaUtil.SHA512Compute(STR_CH), ignoreCase: true);
            Assert.Equal("bedb9ad789a7086b6339543db3f88fabb25aadd213235a9e4a445ba9de437c4ad298f96145770f80190f9b65f53cd9dfdc0a2b3bfc85f046b21c519921be2bfc", ShaUtil.HMacSHA512Compute(STR_CH, SECRET), ignoreCase: true);
        }

        [Fact]
        public void Md5Test()
        {
            Assert.Equal("708171654200ecd0e973167d8826159c", ShaUtil.MD5Compute(STR_EN), ignoreCase: true);
            Assert.Equal("63551bb88226152a2276bf19761b6460", ShaUtil.HMacMD5Compute(STR_EN, SECRET), ignoreCase: true);

            Assert.Equal("86b2826fea7349ce6af96f446d7e2fcf", ShaUtil.MD5Compute(STR_CH), ignoreCase: true);
            Assert.Equal("69bcddfdb4c12c5de290f40c53d31d43", ShaUtil.HMacMD5Compute(STR_CH, SECRET), ignoreCase: true);
        }
    }
}
