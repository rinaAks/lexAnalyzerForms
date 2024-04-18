namespace lexAnalyzerForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum State
        {
            S, I, C, D, E,
            R, B, K, O, M,
            L, F
        }
        string inputText, outputText;
        public string scanLex(string inputStr, int pos) 
        {
            string output = "";
            State currentState = State.S;
            while (true)
            {
                if (currentState == State.S)
                {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        output = inputStr[pos].ToString();
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
                        if (inputStr[pos] == '+') output += "���������� ��������";
                        if (inputStr[pos] == '-') output += "���������� ���������";
                        if (inputStr[pos] == '*') output += "���������� ���������";
                        if (inputStr[pos] == '[') output += "���������� [";
                        if (inputStr[pos] == ']') output += "���������� ]";
                        if (inputStr[pos] == '(') output += "���������� (";
                        if (inputStr[pos] == ')') output += "���������� )";
                        if (inputStr[pos] == '{') output += "���������� {";
                        if (inputStr[pos] == '}') output += "���������� }";
                        if (inputStr[pos] == '=') output += "���������� =";
                        if (inputStr[pos] == '|') output += "���������� |";
                        if (inputStr[pos] == '^') output += "���������� ^";
                        if (inputStr[pos] == ';') output += "���������� ;";
                        if (inputStr[pos] == '\n') output += "���������� enter";
                        currentState = State.F;
                    }
                    else if (inputStr[pos] == '<')
                    {
                        output += "��������� <";
                        currentState = State.R;
                    }
                    else if (inputStr[pos] == '>')
                    {
                        output += "��������� >";
                        currentState = State.R;
                    }
                }

                if (currentState == State.I) {
                    if (inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                    {
                        output += inputStr[pos].ToString();
                        //currentState = State.I;
                        //pos = pos + 1;
                    }
                    else if (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                    {
                        output += inputStr[pos].ToString();
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' || inputStr[pos] == '[' || inputStr[pos] == ']'
                         || inputStr[pos] == ':' || inputStr[pos] == '(' || inputStr[pos] == ')'
                        || inputStr[pos] == '{' || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
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
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' ||
                        inputStr[pos] == ']' || inputStr[pos] == ')'
                        || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\n���������� ����� �����";
                    }
                    else if(inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        currentState = State.F;
                        output += "\n���������� ����� �����";
                    }
                    else if (inputStr[pos] == '.') 
                    {
                        //��� �� �������
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
                        output += '.' + inputStr[pos].ToString();
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
                        output += inputStr[pos].ToString();
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' ||
                         inputStr[pos] == ')'
                        || inputStr[pos] == '}' || inputStr[pos] == ' '
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\n���������� ����� �����";
                    }
                    else if (inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        output += "\n���������� ������������ �����";
                        currentState = State.F;
                    }
                }

                if (currentState == State.R)
                {
                    if(inputStr[pos] == '=')
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

                    if (currentState == State.R) 
                {
                    if ((inputStr[pos] >= 'a' && inputStr[pos] <= 'z') 
                        || (inputStr[pos] >= '0' && inputStr[pos] <= '9')
                        || inputStr[pos] == '-' ||
                        inputStr[pos] == '(' || inputStr[pos] == ' ')
                    {
                        pos = pos - 1;
                        currentState = State.F;
                        output += "\n��������� ���� �������� < ��� >";
                    }
                    if (inputStr[pos] == '=')
                    {
                        currentState = State.F;
                        output += "\n��������� ������� ���� �������� <= ��� >=";
                    }
                    else if (inputStr[pos] == '+' || inputStr[pos] == '.' || inputStr[pos] == '*' || inputStr[pos] == '/' || inputStr[pos] == '[' || inputStr[pos] == ']'
                         || inputStr[pos] == ':' || inputStr[pos] == ')'
                        || inputStr[pos] == '{' || inputStr[pos] == '}' 
                        || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '!'
                        || inputStr[pos] == '|' || inputStr[pos] == '^' || inputStr[pos] == ';' || inputStr[pos] == '\n')
                    {
                        currentState = State.F;
                        output += "\n������";
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
        }
    }
}
