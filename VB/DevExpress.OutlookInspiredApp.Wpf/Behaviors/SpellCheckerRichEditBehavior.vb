Imports System.Globalization
Imports System.IO
Imports DevExpress.Internal
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.SpellChecker
Imports DevExpress.XtraRichEdit.SpellChecker
Imports DevExpress.XtraSpellChecker
Imports DevExpress.XtraSpellChecker.Native

Namespace DevExpress.DevAV
    Public Class SpellCheckerRichEditBehavior
        Inherits Behavior(Of RichEditControl)

        Private spellChecker As SpellChecker
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AssociatedObject.SpellChecker = spellChecker
        End Sub
        Public Sub New()
            spellChecker = CreateSpellChecker()
        End Sub
        Private Function CreateSpellChecker() As SpellChecker
            Dim spellChecker As New SpellChecker()
            spellChecker.Culture = New CultureInfo("en-US")
            spellChecker.SpellCheckMode = SpellCheckMode.AsYouType
            RegisterDictionary(spellChecker, GetDefaultDictionary())
            SpellCheckTextControllersManager.Default.RegisterClass(GetType(RichEditControl), GetType(RichEditSpellCheckController))
            Return spellChecker
        End Function
        Private Function GetDefaultDictionary() As SpellCheckerDictionaryBase
            Dim dic As New SpellCheckerISpellDictionary()
            dic.LoadFromStream(GetFileStream("american.xlg"),GetFileStream("english.aff"),GetFileStream("EnglishAlphabet.txt"))
            Return dic
        End Function
        Private Shared Function GetFileStream(ByVal path As String) As Stream
            Return File.OpenRead(DataDirectoryHelper.GetFile(path, DataDirectoryHelper.DataFolderName))
        End Function
        Private Sub RegisterDictionary(ByVal spellChecker As SpellChecker, ByVal dic As SpellCheckerDictionaryBase)
            dic.Culture = spellChecker.Culture
            spellChecker.Dictionaries.Add(dic)
        End Sub
    End Class
End Namespace
