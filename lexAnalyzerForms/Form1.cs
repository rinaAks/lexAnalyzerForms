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
            public void addValue(object value)
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
        
        public enum State
        {
            S, I, C, D, E,
            R, B, K, O, M,
            L, F
        }
        string inputText, outputText;

        double intBeforeDotNum = 0.0;
        int iterDouble = 0;

        //string tempForWords = ""; 
        string tempForDecWords = ""; Boolean tempForWordsFlag = false; int wordsCounter = 0;
        Boolean newValueFlag = false;
        public List<string> KeywordsList = new List<string> { "if", "else", "while", "arr", "input", "output" };
        private string name;
        int chislo;

        public bool IsKeyword(string word)
        {
            if (KeywordsList.Contains(word)) return true;
            return false;
        }
        bool IsLetter(char symbol)
        {
            if (symbol >= 'a' && symbol <= 'z') return true;
            return false;
        }
        bool IsNumber(char symbol)
        {
            if (symbol >= '0' && symbol <= '9') return true;
            return false;
        }

        public string PrintDetectedCharacter(string sign)
        {
            string forReturn = "";
            if (sign == "+") forReturn += "распознано сложение";
            else if (sign == "-") forReturn += "распознано вычитание";
            else if (sign == "*") forReturn += "распознано умножение";
            else if (sign == "[") forReturn += "распознано [";
            else if (sign == "]") forReturn += "распознано ]";
            else if (sign == "(") forReturn += "распознано (";
            else if (sign == ")") forReturn += "распознано )";
            else if (sign == "{") forReturn += "распознано {";
            else if (sign == "}") forReturn += "распознано }";
            else if (sign == "=") forReturn += "распознано =";
            else if (sign == "|") forReturn += "распознано |";
            else if (sign == "^") forReturn += "распознано ^";
            //if (inputText[pos] == ';') output += "распознано ;";
            else if (sign == "\n") forReturn += "распознано enter";
            else if (IsKeyword(sign))
            {
                forReturn += "\nРаспознано служебное слово " + sign;
            }
            return forReturn;
        }

        public Boolean GetConditionForState(char ch, State st)
        {
            Boolean condition = false;
            if(st == State.S)
            {
                if((ch == '+' || ch == '-' || ch == '*' || ch == '[' || ch == ']' || ch == '(' || ch == ')'
                        || ch == '{' || ch == '}' || ch == '=' || ch == '|') || ch == '^' || ch == ';' || ch == '\n') 
                { condition = true; }
            }
            if(st == State.I)
            {
                if(ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '[' || ch == ']'
                         || ch == ':' || ch == '(' || ch == ')' || ch == '{' || ch == '}' || ch == ' '
                        || ch == '>' || ch == '<' || ch == '=' || ch == '!'
                        || ch == '|' || ch == '^')
                { condition = true; }
            }
            if(st == State.C)
            {
                if(ch == '+' || ch == '-' || ch == '*' || ch == '/' ||
                        ch == ']' || ch == ')' || ch == '}' || ch == ' '
                        || ch == '>' || ch == '<' || ch == '=' || ch == '!'
                        || ch == '|' || ch == '^')
                { condition = true; }
            }
            if(st == State.E)
            {
                if(ch == '+' || ch == '-' || ch == '*' || ch == '/' ||
                         ch == ')'
                        || ch == '}' || ch == ' '
                        || ch == '>' || ch == '<' || ch == '=' || ch == '!'
                        || ch == '|' || ch == '^')
                { condition = true; }
            }
            if(st == State.R)
            {
                if(ch == '+' || ch == '.' || ch == '*' || ch == '/' || ch == '[' || ch == ']'
                         || ch == ':' || ch == ')'
                        || ch == '{' || ch == '}'
                        || ch == '>' || ch == '<' || ch == '!'
                        || ch == '|' || ch == '^' || ch == ';' || ch == '\n')
                { condition = true; }
            }
            return condition;
        }
        int pos;
        public string scanLex(int poss) 
        {
            string output = "";
            name = "";
            State currentState = State.S;
            while (true)
            {
                
                if (currentState == State.S)
                {
                    /*
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
                        else if (IsKeyword(tempForWords))
                        {
                            PrintDetectedCharacter(tempForWords);
                        }
                        else
                        {
                            if (wordsCounter > 0)
                            {
                                if (LexemStorage[wordsCounter - 1].Type == "int" || LexemStorage[wordsCounter - 1].Type == "decimal")
                                {
                                    LexemStorage[wordsCounter - 1].addName(tempForWords);
                                }
                            }
                        }
                        tempForWords = ""; tempForWordsFlag = false; 
                    }
                    */

                    if (IsLetter(inputText[pos]))
                    {
                        name = inputText[pos].ToString();
                        //output = inputText[pos].ToString();
                        //tempForWords = inputText[pos].ToString();
                        currentState = State.I;
                        pos = pos + 1; // точно ли позиция тут меняется?
                    }
                    else if (IsNumber(inputText[pos]))
                    {
                        //output += inputText[pos].ToString();
                        currentState = State.C;
                        chislo = inputText[pos] - '0';
                        pos = pos + 1;
                    }
                    else if (GetConditionForState(inputText[pos], State.S))
                    {
                        // output += '\n';
                        output += PrintDetectedCharacter(inputText[pos].ToString());
                        currentState = State.F;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == ' ')
                    {
                        currentState = State.S;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == '<')
                    {
                        output += "Распознан <";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == '>')
                    {
                        //output += "Распознан >";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == ':')
                    {
                        //output += "Распознан :";
                        currentState = State.B;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == '/')
                    {
                        //output += "Распознан /";
                        currentState = State.K;
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == '!')
                    {
                        //output += "Распознан !";
                        currentState = State.B;
                        pos = pos + 1;
                    }
                }

                if (currentState == State.I) {
                    // здесь из намбер в общий иф
                    if (IsLetter(inputText[pos]) || IsNumber(inputText[pos]))
                    {
                        name += inputText[pos];
                        /*if (inputText.Length - 1-pos > 1)
                        {
                            if ((inputText[pos-1] == 'i') && (inputText[pos] == 'n') 
                                && (inputText[pos + 1] == 't'))
                            {
                                output += "\nint распознан";
                                pos += 2;
                            }
                        }*/
                        //output += inputText[pos].ToString();
                        //tempForWords += inputText[pos].ToString();
                        //currentState = State.I;
                        //pos = pos + 1;
                    }
                    /*
                    else if (IsNumber(inputText[pos]))
                    {
                        output += inputText[pos].ToString();
                        //tempForWords += inputText[pos].ToString();
                    }*/
                    else if (GetConditionForState(inputText[pos], State.I))
                    {
                        //pos = pos - 1;
                        currentState = State.F;
                        //tempForWordsFlag = true;
                        //if (newValueFlag == false)
                        //{
                        //    /*if (tempForWords == "int" || tempForWords == "decimal")
                        //    {
                        //        LexemStorage.Add(new Lexems());
                        //        newValueFlag = true;
                        //    }*/
                        //}
                        //output += inputText[pos].ToString();
                        output += name;
                        output += "Распознан знак или служебное слово";
                    }
                    //else if (inputText[pos] == '.') 
                    //{ 
                    //    currentState = State.F; 
                    //    output += "\nОшибка"; 
                    //}
                    else if (inputText[pos] == ';' || inputText[pos] == '\n')
                    {
                        //pos = pos - 1;
                        currentState = State.F;
                        output += "Распознан знак или служебное слово";
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\nошибка";
                    }
                }

                if (currentState == State.C)
                {
                    if (IsLetter(inputText[pos]) || inputText[pos] == ':' ||
                        inputText[pos] == '{' || inputText[pos] == '[' || inputText[pos] == '(')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (IsNumber(inputText[pos]))
                    {
                        chislo += inputText[pos] - '0';
                        //output += inputText[pos].ToString();
                        //tempForWords += inputText[pos].ToString();
                    }
                    else if (GetConditionForState(inputText[pos], State.C))
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += chislo.ToString();
                        output += "\nРаспознано целое число";
                    }
                    else if(inputText[pos] == ';' || inputText[pos] == '\n')
                    {
                        currentState = State.F;
                        int intNum = 0, tens = 10;
                        /*for(int i = 0; i < tempForWords.Length; i++) 
                        {
                            tens = 1;
                            for(int j = 0; j < tempForWords.Length - 1 - i; j++)
                            {
                                tens *= 10;
                            }
                            intNum += (tempForWords[i]-'0')*tens;
                        }*/
                        //LexemStorage[wordsCounter - 1].addValue(intNum);
                        //intNum = 0;
                        //newValueFlag = false;
                        output += chislo.ToString();
                        output += "\nРаспознано целое число";
                    }
                    else if (inputText[pos] == '.') 
                    {
                        currentState = State.D;
                        pos = pos + 1;
                    }

                }

                //DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                if (currentState == State.D)
                {
                    if (IsLetter(inputText[pos]))
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (IsNumber(inputText[pos]))
                    {
                        double tens = 1.0;
                        /*for (int i = 0; i < tempForWords.Length; i++)
                        {
                            tens = 1;
                            for (int j = 0; j < tempForWords.Length - 1 - i; j++)
                            {
                                tens *= 10;
                            }
                            intBeforeDotNum += (double)(tempForWords[i] - '0') * (double)tens;
                        }*/


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
                    if (IsLetter(inputText[pos]))
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (IsNumber(inputText[pos]))
                    {
                        iterDouble += 1;
                        intBeforeDotNum += (double)(inputText[pos] - '0') * Math.Pow(0.1, iterDouble);
                        output += inputText[pos].ToString();
                        tempForDecWords += inputText[pos].ToString();
                    }
                    else if (GetConditionForState(inputText[pos], State.E))
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознано целое число";
                    }
                    else if (inputText[pos] == ';' || inputText[pos] == '\n')
                    {
                        output += "\nРаспознано вещественное число";
                        tempForDecWords += inputText[pos].ToString();
                        LexemStorage[wordsCounter - 1].addValue(intBeforeDotNum);
                        intBeforeDotNum = 0.0; iterDouble = 0;
                        currentState = State.F;
                    }
                }


                if (currentState == State.R) 
                {
                    if (IsLetter(inputText[pos]) || IsNumber(inputText[pos])
                        || inputText[pos] == '-' ||
                        inputText[pos] == '(' || inputText[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознан знак сравнения < или >";
                    }
                    if (inputText[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\nРаспознан строгий знак сравнени <= или >=";
                    }
                    else if (GetConditionForState(inputText[pos], State.R))
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                }

                if (currentState == State.B)
                {
                    if (inputText[pos] == '=')
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
                    if (IsLetter(inputText[pos]) || IsNumber(inputText[pos])
                        || inputText[pos] == '(' || inputText[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\nРаспознан знак деления /";
                    }
                    if (inputText[pos] == '*')
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

                    if (inputText[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\nРаспознан знак *";
                        pos = pos + 1;
                    }
                    else if (inputText[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else
                    {
                        output += inputText[pos];

                    }
                }

                // работа с комментарием /  MMMMMMMMMMMMMMMMMM
                if (currentState == State.M)
                {

                    if (inputText[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\nРаспознан знак *";
                    }
                    else if (inputText[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\nОшибка";
                    }
                    else if (inputText[pos] == '/')
                    {
                        currentState = State.S;
                        output += "\nРаспознан знак */";
                    }
                    else
                    {
                        output += inputText[pos];
                        //currentState = State.O;
                    }
                }



                if (inputText[pos] == ';')
                {
                    output += "\nРаспознан знак ;";
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
            LexemStorage.Clear();
            //position = 0;
            pos = 0;
            inputText = tbInput.Text;
            //for (int i = 0; i < tbInput.TextLength; i++)
            while (pos < inputText.Length - 2)
            {
                tbOutput.Text += scanLex(pos) + "    position: " + pos.ToString() + '\n';
                //if(position < inputText.Length - 1)
                    //position = position + 1;
            }
            /*tbOutput.Text += "\nСПИСОК ПЕРЕМЕННЫХ";
            for (int i = 0; i < LexemStorage.Count; i++)
            {
                tbOutput.Text += "\n" + LexemStorage[i].Type + " " + LexemStorage[i].Name + " = " + LexemStorage[i].Value;
            }*/
        }
    }
}
