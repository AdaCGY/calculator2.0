using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 計算機renew
{
    public partial class Form1 : Form
    {
        List<string> NumberList_string = new List<string>(); //存取被點擊的數字字串
        List<decimal> NumberList_decimal = new List<decimal>(); //存取數字字串轉型
        List<string> OperatorList = new List<string>(); //存取被點擊的運算符號(加減乘除)
        List<string>HistoryList = new List<string>(); //存取所有被點擊的按鈕  
        string add = "+";
        string subtract = "-";
        string multiply = "x";
        string divide = "÷";
        string decimalpoint = ".";
        string Number; //用以數字處理
        decimal Result;//儲存結果
        public Form1()
        {
            InitializeComponent();
            labelresult.Text = string.Empty;
        }

        private void buttonN0_Click(object sender, EventArgs e)
        {
            Number_Input("0");
        }

        private void buttonN1_Click(object sender, EventArgs e)
        {
            Number_Input("1");
        }

        private void buttonN2_Click(object sender, EventArgs e)
        {
            Number_Input("2");
        }

        private void buttonN3_Click(object sender, EventArgs e)
        {
            Number_Input("3");
        }

        private void buttonN4_Click(object sender, EventArgs e)
        {
            Number_Input("4");
        }

        private void buttonN5_Click(object sender, EventArgs e)
        {
            Number_Input("5");
        }

        private void buttonN6_Click(object sender, EventArgs e)
        {
            Number_Input("6");
        }

        private void buttonN7_Click(object sender, EventArgs e)
        {
            Number_Input("7");
        }

        private void buttonN8_Click(object sender, EventArgs e)
        {
            Number_Input("8");
        }

        private void buttonN9_Click(object sender, EventArgs e)
        {
            Number_Input("9");
        }

        private void button加_Click(object sender, EventArgs e)
        {
            Operator_Input(add);
        }

        private void button減_Click(object sender, EventArgs e)
        {
            Operator_Input(subtract);
        }

        private void button乘_Click(object sender, EventArgs e)
        {
            Operator_Input(multiply);
        }

        private void button除_Click(object sender, EventArgs e)
        {
            Operator_Input(divide);
        }

        private void button點_Click(object sender, EventArgs e)
        {
            Decimal_Point_Input(decimalpoint);
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            DeleteBottonClick();
        }

        private void buttonAC_Click(object sender, EventArgs e)
        {
            ResetBottonClick();
        }

        private void button等於_Click(object sender, EventArgs e)
        {
            EqualBottonClick();
        }
        public void Number_Input(string input)
        {
            HistoryList.Add(input);
            labelresult.Text += input;
        }
        public void Decimal_Point_Input(string input)
        {
            if (labelresult.Text == string.Empty)
            {
                throw new Exception("請先輸入數字再點擊小數點");
            }
            else
            {
                if (labelresult.Text.Last() == '.')
                {
                    return;
                }
                HistoryList.Add(input);
                labelresult.Text += input;
            }
        }
        public void Operator_Input(string input)
        {
            if (labelresult.Text == string.Empty)
            {
                throw new Exception("請先輸入數字再點擊運算符號");
            }
            else
            {
                if (labelresult.Text.Last() == '+' || labelresult.Text.Last() == '-' || labelresult.Text.Last() == 'x' || labelresult.Text.Last() == '÷' || labelresult.Text.Last() == '.')
                {
                    return;
                }
                HistoryList.Add(input);
                labelresult.Text += input;
            }
        }
        public void EqualBottonClick()
        {
            if (labelresult.Text.Contains("="))
            {
                return;
            }
            foreach (var item in HistoryList) //混合數字與運算符號 >>符號夾雜在數字之間，數字列表總數永遠大於運算符號列表總數一個
            {

                if (item != add && item != subtract && item != multiply && item != divide)
                {
                    Number += item;
                }
                if (item == add || item == subtract || item == multiply || item == divide)
                {
                    OperatorList.Add(item);
                    NumberList_string.Add(Number);
                    Number = string.Empty;
                }
            }
            if (Number != string.Empty) //收錄最後一個符號後面的數字
            {
                NumberList_string.Add(Number);
                Number = string.Empty;
            }
            foreach (var item in NumberList_string) //把數字轉型
            {
                var b = decimal.TryParse(item, out decimal number);
                if (b == true)
                {
                    NumberList_decimal.Add(number);
                }
            }
            if (NumberList_decimal.Count >= 2)
            {
                Result = NumberList_decimal[0];
                for (int i = 0; i < OperatorList.Count; i++) //數字總有12個，運算符號就會只有11個
                {
                    if (OperatorList[i] == add)
                    {
                        Result += NumberList_decimal[i + 1];
                    }
                    else if (OperatorList[i] == subtract)
                    {
                        Result -= NumberList_decimal[i + 1];
                    }
                    else if (OperatorList[i] == multiply)
                    {
                        Result *= NumberList_decimal[i + 1];
                    }
                    else if (OperatorList[i] == divide)
                    {
                        if (NumberList_decimal[i + 1] != 0)
                        {
                            Result /= NumberList_decimal[i + 1];
                        }
                        else
                        {
                            throw new Exception("除數不可為0");
                        }
                    }
                }
                labelresult.Text += "=" + Result;
                richTextBox紀錄.Text += labelresult.Text + "\n";
            }
            else
            {
                throw new Exception("計算數值未達兩項，無法進行計算");
            }
        }
        public void DeleteBottonClick()
        {
            if (labelresult.Text.Length > 0 && HistoryList.Count>0)
            {
                HistoryList.RemoveAt(HistoryList.Count-1);
                var length = labelresult.Text.Length;
                labelresult.Text= labelresult.Text.Remove(length-1,1);
            }
        }
        public void ResetBottonClick()
        {
            NumberList_string.Clear();
            NumberList_decimal.Clear();
            OperatorList.Clear();
            HistoryList.Clear();
            labelresult.Text= string.Empty;
        }
    }
}
