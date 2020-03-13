#using <System.dll>
#using <System.Drawing.dll>
#using <System.Windows.Forms.dll>

using namespace System;
using namespace System::Diagnostics;
using namespace System::Windows::Forms;

public ref class Form1: public Form
{
protected:
    TextBox^ textBox1;

// <Snippet1>
private:
    void GetOriginalName()
    {
        // Get the file version for the notepad.
        FileVersionInfo^ myFileVersionInfo =
            FileVersionInfo::GetVersionInfo( Environment::SystemDirectory + "\\Notepad.exe" );

        // Print the original file name.
        textBox1->Text = String::Concat( "Original file name: ", myFileVersionInfo->OriginalFilename );
    }
// </Snippet1>
};
