﻿Imports System.Windows.Forms

Public Class dlg_insertYearLevel

    Private Sub dlg_addsdnt_yes_Clickk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dlg_addsdnt_yes.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub


    Private Sub dlg_addsdnt_no_Click(sender As Object, e As EventArgs) Handles dlg_addsdnt_no.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
    End Sub
End Class
