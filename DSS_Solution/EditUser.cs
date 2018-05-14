﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSS
{
    public partial class EditUser : Form
    {
        private SQLiteConnection dbConnection;

        public EditUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            dbConnection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database.db; Version=3");
            dbConnection.Open();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            setUser();
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            setUser();
            this.Close();
        }

        private void buttonBackToUsersForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setUser()
        {
            Users main = this.Owner as Users;
            try
            {
                //Запись входных данных нового пользователя
                SQLiteCommand setUser = dbConnection.CreateCommand();
                setUser.CommandText = "Insert into Users(Fullname, Post, Login, Password, Role, Etc) values (@Fullname, @Post, @Login, @Password, @Role, @Etc)";
                setUser.Parameters.Add("@Fullname", DbType.String).Value = textBoxUserFullname.Text;
                setUser.Parameters.Add("@Post", DbType.String).Value = textBoxUserPost.Text;
                setUser.Parameters.Add("@Login", DbType.String).Value = textBoxUserLogin.Text;
                setUser.Parameters.Add("@Password", DbType.String).Value = textBoxUserPassword.Text;
                setUser.Parameters.Add("@Role", DbType.String).Value = comboBoxUserRole.SelectedText;
                setUser.Parameters.Add("@Etc", DbType.String).Value = textBoxUserEtc.Text;

                setUser.ExecuteNonQuery();

                if (main != null)
                {
                    //Обновление таблицы с информацией о пользователях
                    try
                    {
                        main.dataGridViewUsers.Refresh();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
