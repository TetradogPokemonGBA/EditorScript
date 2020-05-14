using System.Windows.Controls;
using System.Windows.Documents;

namespace XSE.Wpf
{
    public static class RitchTexboxExtension
    {//source:https://docs.microsoft.com/es-es/dotnet/framework/wpf/controls/how-to-extract-the-text-content-from-a-richtextbox
        public static string GetText(this RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
    }
}
