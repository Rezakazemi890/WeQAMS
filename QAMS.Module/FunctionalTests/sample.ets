;#DropDB QAMSEasyTest

#Application QAMSWin
#Application QAMSWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef QAMSWin

*Action About...
 
*Action OK

#EndIf

#IfDef QAMSWeb
*Action Navigation(My Details)
#EndIf
