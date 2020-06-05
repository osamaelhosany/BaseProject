﻿using System;
using BaseMvvmToolKIt;
using BaseProject.Services.FileSystem;
using BaseProject.Services.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetupDependencyInjection();
            CreateDataBaseTables();
            SetDefaultLanguage();
            SetStartPage();
        }

        private void SetupDependencyInjection()
        {
            IOC.Container.Register<ISqliteService, SqliteService>().AsSingleton();
            IOC.Container.Register<IFileSystem, FileSystem>().AsSingleton();
        }

        private void CreateDataBaseTables()
        {

        }

        private void SetDefaultLanguage()
        {
        }

        private void SetStartPage()
        {

        }
    }
}
