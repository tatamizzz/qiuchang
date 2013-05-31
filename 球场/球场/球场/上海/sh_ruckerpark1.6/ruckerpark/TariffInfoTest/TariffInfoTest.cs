using ruckerpark;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TariffInfoTest
{
    
    
    /// <summary>
    ///这是 TariffInfoTest 的测试类，旨在
    ///包含所有 TariffInfoTest 单元测试
    ///</summary>
    [TestClass()]
    public class TariffInfoTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///TariffInfo 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void TariffInfoConstructorTest()
        {
            bool tof = false; // TODO: 初始化为适当的值
            TariffInfo target = new TariffInfo(tof);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///TariffInfo 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void TariffInfoConstructorTest1()
        {
            TariffInfo target = new TariffInfo();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///getTariffInfo 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ruckerpark.exe")]
        public void getTariffInfoTest()
        {
            TariffInfo_Accessor target = new TariffInfo_Accessor(); // TODO: 初始化为适当的值
            bool tof = false; // TODO: 初始化为适当的值
            target.getTariffInfo(tof);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///cph 的测试
        ///</summary>
        [TestMethod()]
        public void cphTest()
        {
            TariffInfo target = new TariffInfo(); // TODO: 初始化为适当的值
            double expected = 0F; // TODO: 初始化为适当的值
            double actual;
            target.cph = expected;
            actual = target.cph;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///discount 的测试
        ///</summary>
        [TestMethod()]
        public void discountTest()
        {
            TariffInfo target = new TariffInfo(); // TODO: 初始化为适当的值
            double expected = 0F; // TODO: 初始化为适当的值
            double actual;
            target.discount = expected;
            actual = target.discount;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///max_money 的测试
        ///</summary>
        [TestMethod()]
        public void max_moneyTest()
        {
            TariffInfo target = new TariffInfo(); // TODO: 初始化为适当的值
            double expected = 0F; // TODO: 初始化为适当的值
            double actual;
            target.max_money = expected;
            actual = target.max_money;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///min_money 的测试
        ///</summary>
        [TestMethod()]
        public void min_moneyTest()
        {
            TariffInfo target = new TariffInfo(); // TODO: 初始化为适当的值
            double expected = 0F; // TODO: 初始化为适当的值
            double actual;
            target.min_money = expected;
            actual = target.min_money;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
