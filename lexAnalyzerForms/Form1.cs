using System.Xml.Linq;

namespace lexAnalyzerForms
{
    //������������ � ����� ����� �������� � ������ �������, �� string
    //������� ���������, ������� ������ �������, ��� ������� � ��������
    //��������� �����

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

        string tempForWords = "", tempForDecWords = ""; Boolean tempForWordsFlag = false; int wordsCounter = 0;
        Boolean newValueFlag = false;

        public string PrintDetectedCharacter(string sign)
        {
            string forReturn = "";
            if (sign == "+") forReturn += "���������� ��������";
            else if (sign == "-") forReturn += "���������� ���������";
            else if (sign == "*") forReturn += "���������� ���������";
            else if (sign == "[") forReturn += "���������� [";
            else if (sign == "]") forReturn += "���������� ]";
            else if (sign == "(") forReturn += "���������� (";
            else if (sign == ")") forReturn += "���������� )";
            else if (sign == "{") forReturn += "���������� {";
            else if (sign == "}") forReturn += "���������� }";
            else if (sign == "=") forReturn += "���������� =";
            else if (sign == "|") forReturn += "���������� |";
            else if (sign == "^") forReturn += "���������� ^";
            //if (inputStr[pos] == ';') output += "���������� ;";
            else if (sign == "\n") forReturn += "���������� enter";
            else if (sign == "input") { forReturn += "\n���������� ��������� ����� input"; }
            else if (sign == "output") { forReturn += "\n���������� ��������� ����� output"; }
            else if (sign == "if") { forReturn += "\n���������� ��������� ����� if"; }
            else if (sign == "else") { forReturn += "\n���������� ��������� ����� else"; }
            else if (sign == "while") { forReturn += "\n���������� ��������� ����� while"; }
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
                        else if (tempForWords == "input" || tempForWords == "output"
                            || tempForWords == "if" || tempForWords == "else"
                            || tempForWords == "while")
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
                    else if (GetConditionForState(inputStr[pos], State.S))
                    {
                        output += '\n';
                        output += PrintDetectedCharacter(inputStr[pos].ToString());
                        currentState = State.F;
                    }
                    else if (inputStr[pos] == '<')
                    {
                        output += "��������� <";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '>')
                    {
                        //output += "��������� >";
                        currentState = State.R;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == ':')
                    {
                        //output += "��������� :";
                        currentState = State.B;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '/')
                    {
                        //output += "��������� /";
                        currentState = State.K;
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == '!')
                    {
                        //output += "��������� !";
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
                                output += "\nint ���������";
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
                    else if (GetConditionForState(inputStr[pos], State.I))
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        tempForWordsFlag = true;
                        if (newValueFlag == false)
                        {
                            if (tempForWords == "int" || tempForWords == "decimal")
                            {
                                LexemStorage.Add(new Lexems());
                                newValueFlag = true;
                            }
                        }
                        //output += inputStr[pos].ToString();
                        output += "\n��������� ���� ��� ��������� �����";
                    }
                    else if (inputStr[pos] == '.') { currentState = State.F; output += "\n������"; }
                    else if (inputStr[pos] != ';')
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                }

                if (currentState == State.C)
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z') || inputStr[pos] == ':' ||
                        inputStr[pos] == '{' || inputStr[pos] == '[' || inputStr[pos] == '(')
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += inputStr[pos].ToString();
                        tempForWords += inputStr[pos].ToString();
                    }
                    else if (GetConditionForState(inputStr[pos], State.C))
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        
                        output += "\n���������� ����� �����";
                    }
                    else if(inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        currentState = State.F;
                        int intNum = 0, tens = 10;
                        for(int i = 0; i < tempForWords.Length; i++) 
                        {
                            tens = 1;
                            for(int j = 0; j < tempForWords.Length - 1 - i; j++)
                            {
                                tens *= 10;
                            }
                            intNum += (tempForWords[i]-'0')*tens;
                        }
                        LexemStorage[wordsCounter - 1].addValue(intNum);
                        intNum = 0;
                        newValueFlag = false;
                        output += "\n���������� ����� �����";
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
                        output += "\n������";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        double tens = 1.0;
                        for (int i = 0; i < tempForWords.Length; i++)
                        {
                            tens = 1;
                            for (int j = 0; j < tempForWords.Length - 1 - i; j++)
                            {
                                tens *= 10;
                            }
                            intBeforeDotNum += (double)(tempForWords[i] - '0') * (double)tens;
                        }


                        output += '.';
                        currentState = State.E;
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                }

                // EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
                if (currentState == State.E)
                {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        iterDouble += 1;
                        intBeforeDotNum += (double)(inputStr[pos] - '0') * Math.Pow(0.1, iterDouble);
                        output += inputStr[pos].ToString();
                        tempForDecWords += inputStr[pos].ToString();
                    }
                    else if (GetConditionForState(inputStr[pos], State.E))
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\n���������� ����� �����";
                    }
                    else if (inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        output += "\n���������� ������������ �����";
                        tempForDecWords += inputStr[pos].ToString();
                        LexemStorage[wordsCounter - 1].addValue(intBeforeDotNum);
                        intBeforeDotNum = 0.0; iterDouble = 0;
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
                        output += "\n��������� ���� ��������� < ��� >";
                    }
                    if (inputStr[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\n��������� ������� ���� �������� <= ��� >=";
                    }
                    else if (GetConditionForState(inputStr[pos], State.R))
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                }

                if (currentState == State.B)
                {
                    if (inputStr[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\n��������� �������� := ��� !=";
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                }


                // ������ � �������� /  KKKKKKKKKKKKKK
                if (currentState == State.K)
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                        || (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                        || inputStr[pos] == '(' || inputStr[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\n��������� ���� ������� /";
                    }
                    if (inputStr[pos] == '*')
                    {
                        currentState = State.O;
                        output += "\n��������� ���� /*";
                        pos = pos + 1;
                        output += "\n";
                    }
                    else
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                }

                // ������ � ������������ /  OOOOOOOOOOO
                if (currentState == State.O)
                {

                    if (inputStr[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\n��������� ���� *";
                        pos = pos + 1;
                    }
                    else if (inputStr[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                    else
                    {
                        output += inputStr[pos];

                    }
                }

                // ������ � ������������ /  MMMMMMMMMMMMMMMMMM
                if (currentState == State.M)
                {

                    if (inputStr[pos] == '*')
                    {
                        currentState = State.M;
                        output += "\n��������� ���� *";
                    }
                    else if (inputStr[pos] == ';')
                    {
                        currentState = State.F;
                        output += "\n������";
                    }
                    else if (inputStr[pos] == '/')
                    {
                        currentState = State.S;
                        output += "\n��������� ���� */";
                    }
                    else
                    {
                        output += inputStr[pos];
                        //currentState = State.O;
                    }
                }



                if (inputStr[pos] == ';')
                {
                    output += "\n��������� ���� ;";
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
            position = 0;
            inputText = tbInput.Text;
            while (position != inputText.Length - 1)
            {
                tbOutput.Text += scanLex(inputText, position) + "    position: " + position.ToString() + '\n';
                if(position < inputText.Length - 1)
                    position = position + 1;
            }
            tbOutput.Text += "\n������ ����������";
            for (int i = 0; i < LexemStorage.Count; i++)
            {
                tbOutput.Text += "\n" + LexemStorage[i].Type + " " + LexemStorage[i].Name + " = " + LexemStorage[i].Value;
            }
        }
    }
}
