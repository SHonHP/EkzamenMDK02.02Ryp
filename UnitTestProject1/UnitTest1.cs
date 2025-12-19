using Ekzamen0202Ryp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private MainWindow mainWindow = new MainWindow();
        //ввод больших чисел в поля ввода
        [TestMethod]
        public void Test1_LargeNumbers()
        {
            int largeNumber = 999999999;
            double cost1 = mainWindow.CalculateTotalCost(largeNumber, 0);
            double cost2 = mainWindow.CalculateTotalCost(largeNumber, 1);
            Assert.IsTrue(cost1 > 0);
            Assert.IsTrue(cost2 > 0);
        }
        //ввод отрицательных чисел в поля ввода  
        [TestMethod]
        public void Test2_NegativeNumbers()
        {
            //проверка на отрицательные числа
            double cost = mainWindow.CalculateTotalCost(-100, 0);
            Assert.AreEqual(-70.0, cost);
            //проверяем что сверхнорма не считается для отрицательных
            int extra = mainWindow.CalculateSverhNorma(-100, 0);
            Assert.AreEqual(0, extra);
        }
        //пустые поля ввода
        [TestMethod]
        public void Test3_EmptyFields()
        {
            double cost1 = mainWindow.CalculateTotalCost(0, 0);
            double cost2 = mainWindow.CalculateTotalCost(0, 1);
            Assert.AreEqual(0.0, cost1);
            Assert.AreEqual(0.0, cost2);
        }
    }
}
