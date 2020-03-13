﻿' <Snippet1>
' To work correctly with the .aspx file for this sample, name the compiled DLL 
' generated from this class ctrlBuildervb_1.dll.
' Create a namespace that contains two classes that inherit from the
' ControlBuilder class, MyItemControlBuilder and NoWhiteSpaceControlBuilder.
Imports System.Collections
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Security.Permissions

Namespace CustomControlBuildersVB

 ' <Snippet2>
 ' Create a custom ControlBuilder that interprets nested elements
 ' named myitem as TextBoxes. If this class is called in a 
 ' ControlBuilderAttribute applied to a custom control, when
 ' that control is created in a page and it contains child elements
 ' that are named myitem, the child elements will be rendered as 
 ' TextBox server controls. This control builder also ignores literal
 ' strings between elements when it is associated with a control.

 <AspNetHostingPermission(SecurityAction.Demand, _
   Level:=AspNetHostingPermissionLevel.Minimal)> _
 Public NotInheritable Class MyItemControlBuilder
   Inherits ControlBuilder
   
   ' Override the GetChildControlType method to detect
   ' child elements named myitem.
   Public Overrides Function GetChildControlType(ByVal tagName As String, ByVal attribs As IDictionary) As Type
      If [String].Compare(tagName, "myitem", True) = 0 Then
         Return GetType(TextBox)
      End If
      Return Nothing
   End Function 'GetChildControlType
   
   
   ' <Snippet3>
   ' Override the AppendLiteralString method so that literal
   ' text between rows of controls are ignored.  
   Public Overrides Sub AppendLiteralString(s As String)
   End Sub
 End Class
' </Snippet3>
' </Snippet2>
' <Snippet4>
 ' Create a class that does not allow white space generated by a control
 ' to be created as a LiteralControl.   

 <AspNetHostingPermission(SecurityAction.Demand, _
   Level:=AspNetHostingPermissionLevel.Minimal)> _
 Public NotInheritable Class NoWhiteSpaceControlBuilder
   Inherits ControlBuilder
   
   Public Overrides Function AllowWhitespaceLiterals() As Boolean
      Return False
   End Function 'AllowWhitespaceLiterals
 End Class
' </Snippet4>
' <snippet5>
 ' Using the NowWhiteSpaceControlBuilder with a simple control.
 ' When created on a page this control will not allow white space
 ' to be converted into a literal control.     
 <ControlBuilderAttribute(GetType(NoWhiteSpaceControlBuilder))>  _
 <AspNetHostingPermission(SecurityAction.Demand, _
   Level:=AspNetHostingPermissionLevel.Minimal)> _
 Public NotInheritable Class MyNonWhiteSpaceControl
   Inherits Control
 End Class

 ' A simple custom control to compare with MyNonWhiteSpaceControl.
 <AspNetHostingPermission(SecurityAction.Demand, _
   Level:=AspNetHostingPermissionLevel.Minimal)> _
 Public NotInheritable Class WhiteSpaceControl
   Inherits Control
 End Class
' </snippet5>   


' <Snippet6>
 ' Create a control that is modified by the MyItemControlBuilder
 ' class. 
 <ControlBuilderAttribute(GetType(MyItemControlBuilder))>  _
 <AspNetHostingPermission(SecurityAction.Demand, _
   Level:=AspNetHostingPermissionLevel.Minimal)> _
 Public NotInheritable Class MyControl1
   Inherits Control
   ' Create an ArrayList object to store anl the controls
   ' specified as nested elements within this control.
   Private items As New ArrayList()
   
   
   ' This function is internally invoked by
   ' IParserAccessor.AddParsedSubObject(Object).
   ' When called, it adds any TextBox controls to the items
   ' ArrayList object.
   Protected Overrides Sub AddParsedSubObject(obj As [Object])
      If TypeOf obj Is TextBox Then
         items.Add(obj)
      End If
   End Sub
   
   
   ' Override the CreateChildControls to create any TextBox server control
   ' contained within this control as child controls. 
   Protected Overrides Sub CreateChildControls()
      Dim myEnumerator As System.Collections.IEnumerator = items.GetEnumerator()
      While myEnumerator.MoveNext()
         Me.Controls.Add(CType(myEnumerator.Current, TextBox))
      End While
   End Sub
 End Class
' </Snippet6>
End Namespace 'CustomControlBuilders
' </snippet1>