using System;

namespace lab1_CreationalPattern_
{
    abstract class TranslatorCreator
    {
        public string m_inputFileName { get; private set; }
        public string m_outputFileName { get; private set; }
        public TranslatorCreator(string inputFileName, string outputFileName)
        {
            m_inputFileName = inputFileName;
            m_outputFileName = outputFileName;
        }
        public abstract ITranslator createTranslator();
        public void makeTranslation()
        {
            var translator = createTranslator();
            translator.translating(m_inputFileName, m_outputFileName);
        }
    }

    class HtmlCreator : TranslatorCreator
    {
        public HtmlCreator(string inputFileName, string outputFileName) : base(inputFileName, outputFileName)
        {

        }
        public override ITranslator createTranslator()
        {
            return new Html();
        }
    }

    class MarkdownCreator : TranslatorCreator
    {
        public MarkdownCreator(string inputFileName, string outputFileName) : base(inputFileName, outputFileName)
        {

        }
        public override ITranslator createTranslator()
        {
            return new Markdown();
        }
    }
}
