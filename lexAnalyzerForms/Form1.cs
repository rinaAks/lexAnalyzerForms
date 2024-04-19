using System.Xml.Linq;

namespace lexAnalyzerForms
{
    //вещественные и целые числа хранятся в нужном формате, не string
    //создать структуру, которая хранит лексемы, тип лексемы и значение
    //служебные слова

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Lexems
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public object Value { get; set; }

            public void addToStorage(string type, string name, object value)
            {
                Type = type;
                Name = name;
                Value = value;
            }
            public void addType(string type)
            {
                Type = type;
            }
            public void addName(string name)
            {
                Name = name;
            }
            public void addValue(string value)
            {
                Value = value;
            }
            /*public Lexems(string type, string name, object value)
            {
                Type = type;
                Name = name;
                Value = value;
            }*/
            public Lexems()
            {
                Type = "";
                Name = "";
                Value = "";
            }
        }

        enum State
        {
            S, I, C, D, E,
            R, B, K, O, M,
            L, F
        }
        string inputText, outputText;


        string tempForWords = ""; Boolean tempForWordsFlag = false; int wordsCounter = 0;
        Boolean savedValueFlag = false;
        public string scanLex(string inputStr, int pos) 
        {
            string output = ""; 
            State currentState = State.S;
            while (true)
            {
                if (currentState == State.S)
                {
                    if(tempForWords != "" && tempForWordsFlag == true)
                    {
                        if(tempForWords == "int")
                        {
                            wordsCounter += 1;
                            LexemStorage[wordsCounter - 1].addType("int");
                        }
                        else if(tempForWords == "decimal")
                        {
                            wordsCounter += 1;
                            LexemStorage[wordsCounter - 1].addType("decimal");
                        }
                        else
                        {
                            if (LexemStorage[wordsCounter - 1].Type == "int" || LexemStorage[wordsCounter - 1].Type == "decimal")
                            {
                                LexemStorage[wordsCounter - 1].addName(tempForWords);
                            }
                        }
                        tempForWords = ""; tempForWordsFlag = false;
                    }
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        output = inputStr[pos].ToString();
                        tempForWords = inputStr[pos].ToString();
                        currentState = State.I;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        //output += inputStr[pos].ToString();
                        currentState = State.C;
                    }
                    else if ((inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '[' || inputStr[pos] == ']' || inputStr[pos] == '(' || inputStr[pos] == ')'
                        || inputStr[pos] == '{' || inputStr[pos] == '}' || inputStr[pos] == '=' || inputStr[pos] == '|') || inputStr[pos] == '^' || inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        output += '\n';
                        if (inputStr[pos] == '+') output += "распознано сложение";
                        if (inputStr[pos] == '-') output += "распознано вычитание";
                        if (inputStr[pos] == '*') output += "распознано умножение";
                        if (inputStr[pos] == '[') output += "распознано [";
                        if (inputStr[pos] == ']') output += "распознано ]";
                        if (inputStr[pos] == '(') output += "распознано (";
                        if (inputStr[pos] == ')') output += "распознано )";
                        if (inputStr[pos] == '{') output += "распознано {";
                        if (inputStr[pos] == '}') output += "распознано }";
                        if (inputStr[pos] == '=') output += "распознано =";
                        if (inputStr[pos] == '|') output += "распознано |";
                        if (inputStr[pos] == '^') output += "распознано ^";
                        if (inputStr[pos] == ';') output += "распознано ;";
                        if (inputStr[pos] == '\n') output += "распознано enter";
                        currentState = State.F;
                    }
                    else if (inputStr[pos] == '<')
                    {
                        output += "Распознан <";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '>')
                    {
                        //output += "Распознан >";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == ':')
                    {
                        //output += "Распознан :";
                        currentState = State.B;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '/')
                    {
                        //output += "Распознан /";
                        currentState = State.K;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '!')
                    {
                        //output += "Распознан !";
                        currentState = State.B;
                        pos = pos + 1;
                    }
                }

                if (currentState == State.I) {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        /*if (inputStr.Length - 1-pos > 1)
                        {
                            if ((inputStr[pos-1] == 'i') && (inputStr[pos] == 'n') 
                                && (inputStr[pos + 1] == 't'))
                            {
                                output += "\nint распознан";
                                pos += 2;
                            }
                        }*/
                        output += inputStr[pos].ToString();
                        tempForWords += inputStr[pos].ToString();
                        //currentState = State.I;
                        //pos = pos + 1;
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += inputStr[pos].ToString();
                        tempForWords += inputStr[pos].ToString();
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' || inputStr[pos] == '[' || inputStr[pos] == ']'
                         || inputStr[pos] == ':' || inputStr[pos] == '(' || inputStr[pos] == ')'
                        || inputStr[pos] == '{' || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        tempForWordsFlag = true; LexemStorage.Add(new Lexems()); 
                        //output += inputStr[pos].ToString();
                        output += "\nРаспознан знак или служебное слово";
                    }
                    else if (inputStr[pos] == '.') { currentState = State.F; output += "\nОшибка"; }
                    else if (inputStr[pos] != ';')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }

                if (currentState == State.C)
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z') || inputStr[pos] == ':' ||
                        inputStr[pos] == '{' || inputStr[pos] == '[' || inputStr[pos] == '(')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += inputStr[pos].ToString();
                        tempForWords += inputStr[pos].ToString();
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' ||
                        inputStr[pos] == ']' || inputStr[pos] == ')'
                        || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        if(LexemStorage[wordsCounter - 1].Name != "" )
                        {
                            output += "\nAAAAAAAAAAAAA";
                            LexemStorage[wordsCounter - 1].addValue(tempForWords);
                        }
                        output += "\nРаспознано целое число";
                    }
                    else if(inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        currentState = State.F;
                        output += "\nРаспознано целое число";
                    }
                    else if (inputStr[pos] == '.') 
                    {
                        currentState = State.D;
                        pos = pos + 1;
                    }

                }


                //DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                if (currentState == State.D)
                {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += '.';
                        currentState = State.E;
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }

                // EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
                if (currentState == State.E)
                {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += inputStr[pos].ToString();
                        string a = "5";
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' ||
                         inputStr[pos] == ')'
                        || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознано целое число";
                    }
                    else if (inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        output += "\nРаспознано вещественное число";
                        currentState = State.F;
                    }
                }


                if (currentState == State.R) 
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z') 
                        || (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                        || inputStr[pos] == '-' ||
                        inputStr[pos] == '(' || inputStr[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознан знак сравнения < или >";
                    }
                    if (inputStr[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\nРаспознан строгий знак сравнени <= или >=";
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '.' || inputStr[pos] == '*' || inputStr[pos] == '/' || inputStr[pos] == '[' || inputStr[pos] == ']'
                         || inputStr[pos] == ':' || inputStr[pos] == ')'
                        || inputStr[pos] == '{' || inputStr[pos] == '}' 
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^' || inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }

                if (currentState == State.B)
                {
                    if (inputStr[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\nРаспознан оператор := или !=";
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }


                // работа с символом /  KKKKKKKKKKKKKK
                if (currentState == State.K)
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                        || (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                        || inputStr[pos] == '(' || inputStr[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознан знак деления /";
                    }
                    if (inputStr[pos] == '*')
                    {
                        currentState = State.O;
                        output += "\nРаспознан знак /*";
                        pos = pos + 1;
                        output += "\n";
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }

                // работа с комментарием /  OOOOOOOOOOO
                if (currentState == State.O)
                {

                    if (inputStr[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\nРаспознан знак *";
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else
                    {
                        output += inputStr[pos];

                    }
                }

                // работа с комментарием /  MMMMMMMMMMMMMMMMMM
                if (currentState == State.M)
                {

                    if (inputStr[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\nРаспознан знак *";
                    }
                    else if (inputStr[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (inputStr[pos] == '/')
                    {
                        currentState = State.S;
                        output += "\nРаспознан знак */";
                    }
                    else
                    {
                        output += inputStr[pos];
                        //currentState = State.O;
                    }
                }



                if (inputStr[pos] == ';')
                {
                    currentState = State.F;
                }
                if(currentState == State.F) { break; }
                else
                {
                    pos = pos + 1;
                }
            }
            position = pos;
            return output;
        }
        public static int position = 0;
        public List<Lexems> LexemStorage = new List<Lexems>();
        private void button1_Click(object sender, EventArgs e)
        {
            position = 0;
            inputText = tbInput.Text;
            while (position != inputText.Length - 1)
            {
                tbOutput.Text += scanLex(inputText, position) + "    position: " + position.ToString() + '\n';
                if(position < inputText.Length - 1)
                    position = position + 1;
            }
            tbOutput.Text += "\nСПИСОК ПЕРЕМЕННЫХ";
            for (int i = 0; i < LexemStorage.Count-1; i++)
            {
                tbOutput.Text += "\n" + LexemStorage[i].Type + " " + LexemStorage[i].Name + " = " + LexemStorage[i].Value;
            }
        }
    }
}
