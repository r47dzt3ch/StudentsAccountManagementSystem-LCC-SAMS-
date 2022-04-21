﻿Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frm_slists
    Dim sAccount As New frm_SAccounts
    Sub random()

        'Create an instance of the Random class
        Dim rnd As New Random()

        'Get a random number from 10 to 99  (2 digits)
        randomNumber = rnd.Next(10, 100000)
    End Sub

    Dim randomNumber As Integer

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim SelecteDepartment As Integer = TabControl1.SelectedIndex
        Select Case SelecteDepartment
            Case 0
                txtb_studId.Clear()
                txtb_studFname.Clear()
                txtb_studLname.Clear()
                txtb_studMI.Clear()
                txtb_RpU.Clear()
                txtb_noUnits.Clear()
                txtb_Search.Clear()
                cbo_SearchBy.SelectedIndex = 1
                cbo_schyear.SelectedIndex = -1
                cbo_sem.SelectedIndex = -1
                cbo_yearlevel.SelectedIndex = -1
                cbo_course.SelectedIndex = -1

                _dbConnection("db_lccsams")
                _displayRecords(sStudR, dg_studR)
                cbo_SearchBy.SelectedIndex = 1

            Case 1
                _loadToCombobox(eSelect_SY, cbo_eSYName)
                _loadToCombobox(eSelect_GL, cbo_eg)
                cbo_eSearchBY.SelectedIndex = 1
                txtb_eStudID.Clear()
                txtb_eStudFname.Clear()
                txtb_estudLname.Clear()
                txtb_estudMI.Clear()
                cbo_eSYName.SelectedIndex = -1
                cbo_eGradeLevel.SelectedIndex = -1

                _dbConnection("db_lccsams")
                _displayRecords(eSelect_studRec, dg_eStudRecords)
                cbo_eSearchBY.SelectedIndex = 1

        End Select
    End Sub

    Private Sub btn_addNstud_Click(sender As Object, e As EventArgs) Handles btn_addNstud.Click
        If MessageBox.Show("", "Do You want to add new student", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            txtb_studFname.Enabled = True
            txtb_studLname.Enabled = True
            txtb_studMI.Enabled = True
            txtb_noUnits.Enabled = True
            cbo_schyear.Enabled = True
            cbo_yearlevel.Enabled = True
            cbo_sem.Enabled = True
            cbo_course.Enabled = True
            dg_studR.Enabled = False
            btn_gotoAcct.Enabled = False
            btn_cancel.Enabled = True

            'clear selection
            txtb_studFname.Clear()
            txtb_studLname.Clear()
            txtb_studMI.Clear()
            cbo_yearlevel.SelectedIndex = -1
            cbo_sem.SelectedIndex = -1
            cbo_course.SelectedIndex = -1
            txtb_noUnits.Clear()
            txtb_RpU.Clear()

            random()
            txtb_studId.Text = randomNumber
            btn_save.Enabled = True
            btn_addNstud.Enabled = False
            btn_updtStud.Enabled = False
            a = 1

        End If
    End Sub
    Dim a As Integer
    Sub _insertdataOfStudents()
        Try
            _dbConnection("db_lccsams")
            Dim sql As String = "select stud_id from tbl_student where stud_Fname='" & txtb_studFname.Text & "' and stud_Lname='" & txtb_studLname.Text & "' "
            Dim querry2 = "insert into tbl_studnounits values ('" & randomNumber & "','" & txtb_noUnits.Text & "','" & txtb_RpU.Text & "','" & cbo_schyear.SelectedValue & "','" & cbo_sem.SelectedValue & "','" & cbo_yearlevel.SelectedValue & "','" & cbo_course.SelectedValue & "')"
            dbConn.Open()
            sqlCommand = New MySqlCommand(sql, dbConn)
            dr = sqlCommand.ExecuteReader
            If dr.HasRows Then
                MessageBox.Show("This data already Exist!")
            Else
                dbConn.Close()
                Dim querry1 = "insert into tbl_student (stud_id,stud_Fname,stud_Lname,stud_midI,crs_id,sem_id,yl_id,sy_id,educ_level) values('" & randomNumber & "','" & txtb_studFname.Text & "','" & txtb_studLname.Text & "','" & txtb_studMI.Text & "','" & cbo_course.SelectedValue & "','" & cbo_sem.SelectedValue & "','" & cbo_yearlevel.SelectedValue & "','" & cbo_schyear.SelectedValue & "','" & 0 & "')"
                _insertData(querry1)
                _insertData(querry2)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: ", ex.Message)
        Finally
            dbConn.Close()
        End Try
    End Sub
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If txtb_studFname.Text = "" Or txtb_studLname.Text = "" Or cbo_course.Text = "" Or cbo_yearlevel.Text = "" Or cbo_sem.Text = "" Then
            MessageBox.Show("Please fill-up all fields!")
        Else
            txtb_studFname.Enabled = False
            txtb_studLname.Enabled = False
            txtb_studMI.Enabled = False
            cbo_schyear.Enabled = False
            cbo_yearlevel.Enabled = False
            cbo_sem.Enabled = False
            cbo_course.Enabled = False
            txtb_noUnits.Enabled = False
            btn_cancel.Enabled = False
            Select Case a
                Case 1
                    _insertdataOfStudents()
                    dlg_savesuccessfully.ShowDialog()
                    _displayRecords(sStudR, dg_studR)
                    _loadToCombobox(slctC, cbo_course)
                    _loadToCombobox(slctS, cbo_sem)
                    _loadToCombobox(slctYL, cbo_yearlevel)
                    _loadToCombobox(slctSY, cbo_schyear)

                    btn_save.Enabled = False
                    btn_addNstud.Enabled = True
                    btn_updtStud.Enabled = True
                    dg_studR.Enabled = True
                    btn_gotoAcct.Enabled = True

                Case 2
                    Dim querry3 = "update tbl_student set stud_Fname='" & txtb_studFname.Text & "',stud_Lname='" & txtb_studLname.Text & "',stud_midI='" & txtb_studMI.Text & "',crs_id='" & cbo_course.SelectedValue & "',sem_id='" & cbo_sem.SelectedValue & "',yl_id='" & cbo_yearlevel.SelectedValue & "',sy_id='" & cbo_schyear.SelectedValue & "' where stud_id='" & stud_id & "' "
                    Dim querry4 = "update tbl_studnounits set st_noUnits='" & txtb_noUnits.Text & "',st_rateperunit='" & txtb_RpU.Text & "',crs_id='" & cbo_course.SelectedValue & "',sem_id='" & cbo_sem.SelectedValue & "',yl_id='" & cbo_yearlevel.SelectedValue & "',sy_id='" & cbo_schyear.SelectedValue & "'  where stud_id='" & stud_id & "' "
                    _dbConnection("db_lccsams")

                    _updateData(querry3)
                    _updateData(querry4)
                    UpdatedSuccessfully.ShowDialog()
                    _displayRecords(sStudR, dg_studR)
                    btn_updtStud.Enabled = True
                    btn_save.Enabled = False
                    btn_addNstud.Enabled = True
                    dg_studR.Enabled = True
                    btn_gotoAcct.Enabled = True
            End Select


        End If


    End Sub

    Private Sub btn_updtStud_Click(sender As Object, e As EventArgs) Handles btn_updtStud.Click
        If MessageBox.Show("", "Do You want to Update a Student?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            txtb_studFname.Enabled = True
            txtb_studLname.Enabled = True
            txtb_studMI.Enabled = True
            txtb_noUnits.Enabled = True
            cbo_schyear.Enabled = True
            cbo_yearlevel.Enabled = True
            cbo_sem.Enabled = True
            cbo_course.Enabled = True
            btn_updtStud.Enabled = False
            btn_save.Enabled = True
            btn_addNstud.Enabled = False
            dg_studR.Enabled = False
            btn_gotoAcct.Enabled = False
            btn_cancel.Enabled = True
            a = 2


            cbo_course.Text = ""
            cbo_yearlevel.Text = ""
            cbo_sem.Text = ""
            cbo_schyear.Text = ""
            txtb_noUnits.Clear()
            txtb_RpU.Clear()
        End If
    End Sub

    Private Sub btn_enter_Click(sender As Object, e As EventArgs)
        'Dim querry1 = "select "
        '_dbConnection("db_lccsams")
        '_displayRecords(querry1, dg_studR)
    End Sub

    Private Sub dg_studR_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_studR.CellClick
        Try
            Dim i = e.RowIndex
            With dg_studR

                stud_id = .Item("s_id", i).Value
                stud_name = .Item("s_fName", i).Value.ToString.ToUpper + " " + .Item("s_Midi", i).Value.ToString.ToUpper + " " + .Item("s_lName", i).Value.ToString.ToUpper

                txtb_studId.Text = .Item("s_id", i).Value
                Dim querry As String = "Select c.crs_name from tbl_student s inner join tbl_coll_course c on s.crs_id=c.crs_id where s.stud_id = '" & txtb_studId.Text & "'"
                Dim querry2 As String = "Select y.yl_name from tbl_student s inner join tbl_year_level y on s.yl_id=y.yl_id where s.stud_id = '" & txtb_studId.Text & "'"
                Dim querry3 As String = "Select sem.sem_name from tbl_student s inner join tbl_semester sem  on s.sem_id=sem.sem_id where s.stud_id = '" & txtb_studId.Text & "'"
                Dim querry4 As String = "Select sy.sy_name from tbl_student s inner join tbl_sch_year sy  on s.sy_id=sy.sy_id where s.stud_id = '" & txtb_studId.Text & "' group by sy_name having count(*) > 0"
                Dim querry6 As String = "select  st_rateperunit  from tbl_studnounits where stud_id = '" & stud_id & "' and sy_id='" & .Item("sy_id", i).Value & "' and sem_id='" & .Item("sem_id", i).Value & "' and yl_id='" & .Item("yl_id", i).Value & "' and  crs_id='" & .Item("crs_id", i).Value & "'"
                Dim querry5 As String = "select  st_noUnits from tbl_studnounits where stud_id ='" & stud_id & "' and sy_id='" & .Item("sy_id", i).Value & "' and sem_id='" & .Item("sem_id", i).Value & "' and yl_id='" & .Item("yl_id", i).Value & "' and  crs_id='" & .Item("crs_id", i).Value & "'"
                _dbConnection("db_lccsams")
                txtb_studFname.Text = .Item("s_fName", i).Value
                txtb_studLname.Text = .Item("s_lName", i).Value
                txtb_studMI.Text = .Item("s_Midi", i).Value

                txtb_noUnits.Clear()
                txtb_RpU.Clear()
                _loadToTextbox(querry5, txtb_noUnits)
                _loadToTextbox(querry6, txtb_RpU)

                _selectComboBoxText(querry, cbo_course)
                _selectComboBoxText(querry2, cbo_yearlevel)
                _selectComboBoxText(querry3, cbo_sem)
                _selectComboBoxText(querry4, cbo_schyear)

                stud_sy = cbo_schyear.Text
                stud_sem = cbo_sem.Text
                stud_yl = cbo_yearlevel.Text
                stud_crs = cbo_course.Text
                stud_nUnits = txtb_noUnits.Text
                stud_rpu = txtb_RpU.Text

                sy_fid = .Item("sy_id", i).Value
            End With

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub txtb_Search_KeyUp(sender As Object, e As KeyEventArgs) Handles txtb_Search.KeyUp
        Select Case cbo_SearchBy.SelectedItem.ToString
            Case "Name"
                _dbConnection("db_lccsams")
                _displayRecords(sStudR + " where stud_Lname Like '%" & txtb_Search.Text & "%' or stud_Fname  Like '%" & txtb_Search.Text & "%' ", dg_studR)

            Case "ID Number"
                _dbConnection("db_lccsams")
                _displayRecords(sStudR + " where stud_id Like '" & txtb_Search.Text & "%' ", dg_studR)
        End Select
    End Sub
    Private Sub btn_gotoAcct_Click(sender As Object, e As EventArgs) Handles btn_gotoAcct.Click
        Try
            If stud_id = "" Then
                MessageBox.Show("Pag select sag estudyanto bago ka mo proceed")
            Else
                current_menu = 2
                Dashboard.btn_menuStudentsAccount_click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Dim rpu_id As Integer = 1
    Private Sub cbo_course_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbo_course.SelectionChangeCommitted
        txtb_RpU.Clear()
        Dim querry2 As String = "Select fees_amount from tbl_coll_fees where sy_id='" & cbo_schyear.SelectedValue & "' and sem_id='" & cbo_sem.SelectedValue & "' and yl_id='" & cbo_yearlevel.SelectedValue & "' and  crs_id='" & cbo_course.SelectedValue & "' and tuition_rpu_id='" & rpu_id & "'"
        If cbo_sem.Text <> "" Then
            _dbConnection("db_lccsams")
            txtb_RpU.Clear()
            _loadToTextbox(querry2, txtb_RpU)
        End If
    End Sub

    Private Sub cbo_yearlevel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbo_yearlevel.SelectionChangeCommitted
        cbo_sem.SelectedIndex = -1
        cbo_course.SelectedIndex = -1
    End Sub

    Private Sub cbo_sem_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbo_sem.SelectionChangeCommitted
        cbo_course.SelectedIndex = -1
    End Sub


    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        'disabled
        txtb_studFname.Enabled = False
        txtb_studLname.Enabled = False
        txtb_studMI.Enabled = False
        cbo_schyear.Enabled = False
        cbo_yearlevel.Enabled = False
        cbo_sem.Enabled = False
        cbo_course.Enabled = False
        txtb_noUnits.Enabled = False
        'clear selection
        txtb_studFname.Clear()
        txtb_studLname.Clear()
        txtb_studMI.Clear()
        cbo_schyear.SelectedIndex = -1
        cbo_yearlevel.SelectedIndex = -1
        cbo_course.SelectedIndex = -1
        cbo_sem.SelectedIndex = -1

        txtb_noUnits.Clear()
        txtb_RpU.Clear()
        txtb_studId.Clear()

        btn_save.Enabled = False
        btn_addNstud.Enabled = True
        btn_updtStud.Enabled = True
        dg_studR.Enabled = True
        btn_gotoAcct.Enabled = True
        btn_cancel.Enabled = False
    End Sub




    Private Sub frm_slists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _dbConnection("db_lccsams")
        _loadToCombobox(slctC, cbo_course)
        _loadToCombobox(slctS, cbo_sem)
        _loadToCombobox(slctYL, cbo_yearlevel)
        _displayRecords(sStudR, dg_studR)
        _loadToCombobox(slctSY, cbo_schyear)
        cbo_SearchBy.SelectedIndex = 1
        cbo_schyear.SelectedIndex = -1
        cbo_sem.SelectedIndex = -1
        cbo_yearlevel.SelectedIndex = -1
        cbo_course.SelectedIndex = -1
    End Sub


    ''Elementary Section ########################################################################################################################################################
    Dim b As Integer
    Private Sub txtb_eSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtb_eSearch.KeyUp
        Select Case cbo_SearchBy.SelectedItem.ToString
            Case "Name"
                _dbConnection("db_lccsams")
                _displayRecords(" select * from tbl_elem_students where estud_fname Like '%" & txtb_eSearch.Text & "%' or estud_lname  Like '%" & txtb_eSearch.Text & "%' ", dg_eStudRecords)

            Case "ID Number"
                _dbConnection("db_lccsams")
                _displayRecords("select * from tbl_elem_students where estud_id Like '" & txtb_eSearch.Text & "%' ", dg_eStudRecords)
        End Select
    End Sub


    Private Sub btn_eViewAccount_Click(sender As Object, e As EventArgs) Handles btn_eViewAccount.Click
        Try
            If estud_id = "" Then
                MessageBox.Show("Pag select sag estudyanto bago ka mo proceed")
            Else
                current_menu = 2
                Dashboard.btn_menuStudentsAccount_click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_eSave_Click(sender As Object, e As EventArgs) Handles btn_eSave.Click
        If txtb_eStudFname.Text = "" Or txtb_eStudFname.Text = "" Or cbo_eGradeLevel.Text = "" Or cbo_eSYName.Text = "" Then
            MessageBox.Show("Please fill-up all fields!")
        Else
            txtb_eStudFname.Enabled = False
            txtb_eStudFname.Enabled = False
            txtb_estudMI.Enabled = False
            cbo_eGradeLevel.Enabled = False
            cbo_eSYName.Enabled = False
            btn_eSave.Enabled = False
            btn_eViewAccount.Enabled = True
            btn_eAddNewStud.Enabled = True
            btn_eUpdateNewStud.Enabled = True
            dg_eStudRecords.Enabled = True

            Select Case b
                Case 1
                    _insertdataOfStudents()
                    dlg_savesuccessfully.ShowDialog()
                    _displayRecords(sStudR, dg_eStudRecords)
                Case 2
                    Dim querry3 = "update tbl_student set stud_Fname='" & txtb_studFname.Text & "',stud_Lname='" & txtb_studLname.Text & "',stud_midI='" & txtb_studMI.Text & "',crs_id='" & cbo_course.SelectedValue & "',sem_id='" & cbo_sem.SelectedValue & "',yl_id='" & cbo_yearlevel.SelectedValue & "',sy_id='" & cbo_schyear.SelectedValue & "' where stud_id='" & stud_id & "' "
                    _dbConnection("db_lccsams")
                    _updateData(querry3)
                    _displayRecords(sStudR, dg_eStudRecords)
                    UpdatedSuccessfully.ShowDialog()
            End Select
        End If

    End Sub

    Private Sub btn_eAddNewStud_Click(sender As Object, e As EventArgs) Handles btn_eAddNewStud.Click
        If MessageBox.Show("", "Do You want to add new student", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            txtb_eStudFname.Enabled = True
            txtb_estudLname.Enabled = True
            txtb_estudMI.Enabled = True

            cbo_eSYName.Enabled = True
            cbo_eGradeLevel.Enabled = True
            dg_eStudRecords.Enabled = False
   

            'clear selection
            txtb_eStudFname.Clear()
            txtb_estudLname.Clear()
            txtb_estudMI.Clear()
            cbo_eSYName.SelectedIndex = -1
            cbo_sem.SelectedIndex = -1
            cbo_eGradeLevel.SelectedIndex = -1
            random()
            txtb_studId.Text = randomNumber

            btn_eAddNewStud.Enabled = False
            btn_eCancel.Enabled = True
            btn_eSave.Enabled = True
            btn_eAddNewStud.Enabled = False
            btn_eUpdateNewStud.Enabled = False
            b = 1

        End If

    End Sub

    Private Sub btn_eUpdateNewStud_Click(sender As Object, e As EventArgs) Handles btn_eUpdateNewStud.Click
        If MessageBox.Show("", "Do You want to Update a Student?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            txtb_eStudFname.Enabled = True
            txtb_estudLname.Enabled = True
            txtb_estudMI.Enabled = True
            cbo_eSYName.Enabled = True
            cbo_eGradeLevel.Enabled = True
            dg_eStudRecords.Enabled = False

            btn_eUpdateNewStud.Enabled = False
            btn_eSave.Enabled = True
            btn_eAddNewStud.Enabled = False
            dg_eStudRecords.Enabled = False
            btn_eViewAccount.Enabled = False
            btn_eCancel.Enabled = True
            b = 2


        End If

    End Sub

    Private Sub btn_eCancel_Click(sender As Object, e As EventArgs) Handles btn_eCancel.Click
        'disabled
        txtb_eStudFname.Enabled = False
        txtb_estudLname.Enabled = False
        txtb_estudMI.Enabled = False
        cbo_eGradeLevel.Enabled = False
        cbo_eSYName.Enabled = False
        'clear selection
        txtb_eStudFname.Clear()
        txtb_estudLname.Clear()
        txtb_estudMI.Clear()
        cbo_eGradeLevel.SelectedIndex = -1
        cbo_eSYName.SelectedIndex = -1

        btn_eSave.Enabled = False
        btn_eAddNewStud.Enabled = True
        btn_eUpdateNewStud.Enabled = True
        dg_eStudRecords.Enabled = True
        btn_eViewAccount.Enabled = True
        btn_eCancel.Enabled = False
    End Sub

    Private Sub dg_eStudRecords_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_eStudRecords.CellClick
        Try
            Dim i = e.RowIndex
            With dg_eStudRecords

                Dim querry As String = "Select esy_name from tbl_elem_sy where esy_id='" & .Item("col_esy_id", i).Value & "'"
                Dim querry2 As String = "Select egl_name from tbl_elem_gradelevel where egl_id='" & .Item("col_egl_id", i).Value & "'"
                _dbConnection("db_lccsams")
                eStud_id = .Item("col_estud_id", i).Value
                eStudname = .Item("col_estud_fname", i).Value.ToString.ToUpper + " " + .Item("col_estud_mi", i).Value.ToString.ToUpper + " " + .Item("col_estud_lname", i).Value.ToString.ToUpper
                eStudSY = .Item("col_estud_id", i).Value
                eStudGL = .Item("col_estud_id", i).Value

                txtb_eStudID.Text = .Item("col_estud_id", i).Value
                txtb_eStudFname.Text = .Item("col_estud_fname", i).Value.ToString.ToUpper
                txtb_estudLname.Text = .Item("col_estud_lname", i).Value.ToString.ToUpper
                txtb_estudMI.Text = .Item("col_estud_mi", i).Value.ToString.ToUpper

                _selectComboBoxText(querry, cbo_eSYName)
                _selectComboBoxText(querry2, cbo_eGradeLevel)

            End With

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
End Class