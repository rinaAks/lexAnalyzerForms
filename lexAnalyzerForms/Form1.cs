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
                if (currentState == State.S && inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                {
                    output = inputStr[pos].ToString();
                    currentState = State.I;
                    pos = pos + 1;
                }
                if (currentState == State.S && (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '[' || inputStr[pos] == ']' || inputStr[pos] == '(' || inputStr[pos] == ')'
                    || inputStr[pos] == '{' || inputStr[pos] == '}' || inputStr[pos] == '=' || inputStr[pos] == '|') || inputStr[pos] == '^' || inputStr[pos] == ';' || inputStr[pos] == '\n')
                {
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
                
                if (currentState == State.I && inputStr[pos] >= 'a' && inputStr[pos] <= 'z')
                {
                    output += inputStr[pos].ToString();
                    //currentState = State.I;
                    //pos = pos + 1;
                }
                if (currentState == State.I && inputStr[pos] >= '0' && inputStr[pos] <= '9')
                {
                    output += inputStr[pos].ToString();
                }
                if (currentState == State.I && (inputStr[pos] == '+' || inputStr[pos] == '-' || inputStr[pos] == '*' || inputStr[pos] == '/' || inputStr[pos] == '[' || inputStr[pos] == ']' || inputStr[pos] == '(' || inputStr[pos] == ')'
                    || inputStr[pos] == '{' || inputStr[pos] == '}' || inputStr[pos] == ' '
                    || inputStr[pos] == '>' || inputStr[pos] == '<' || inputStr[pos] == '=' || inputStr[pos] == '!'
                    || inputStr[pos] == '|' || inputStr[pos] == '^'))
                {
                    pos = pos - 1;
                    currentState = State.F;
                    //output += inputStr[pos].ToString();
                    output += "Распознана лексема; поиск служебного слова";
                }
                if (inputStr[pos] == '"')
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
