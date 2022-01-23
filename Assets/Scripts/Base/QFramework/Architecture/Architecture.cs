using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace QZVR {

    public interface IArchitecture {
        /// <summary>
        /// ????????
        /// </summary>
        void RegisterSystem<T>(T Sytem) where T : ISystem;

        /// <summary>
        /// ????Model
        /// </summary>
        void RegisterModel<T>(T model) where T : IModel;

        /// <summary>
        /// ????Utility
        /// </summary>
        void RegisteUtility<T>(T utility);

        /// <summary>
        /// ????Model
        /// </summary>
        T GetModel<T>() where T : class, IModel;

        /// <summary>
        /// ????Utility
        /// </summary>
        T GetUtility<T>() where T : class;
        /// <summary>
        /// ????System
        /// </summary>
        T GetSystem<T>() where T : class;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        /// <summary>
        /// ????????
        /// </summary>
        void SendEvent<T>() where T : new(); // +

        /// <summary>
        /// ????????
        /// </summary>
        void SendEvent<T>(T e); // +

        /// <summary>
        /// ????????
        /// </summary>
        IUnRegister RegisterEvent<T>(Action<T> onEvent); // +

        /// <summary>
        /// ????????
        /// </summary>
        void UnRegisterEvent<T>(Action<T> onEvent); // +
    }

    public abstract class Architecture<T> :IArchitecture where T : Architecture<T>, new() {

        /// <summary>
        /// ??????????????
        /// </summary>
        private bool m_Inited = false;

        /// <summary>
        /// ????????
        /// </summary>
        public static Action<T> OnRegisterPatch = architecture => { };

        /// <summary>
        /// ??????????Model
        /// </summary>
        private List<IModel> m_Models = new List<IModel>();
        /// <summary>
        /// ??????????Sytem
        /// </summary>
        private List<ISystem> m_Sytems = new List<ISystem>();

    
        private static T m_Architecture = null;

        

        public  static SQLiteHelper SQLite;

        public static IArchitecture Instance {
            get {
                if(m_Architecture == null) {
                    MakeSureArchitecture();
                }
                return m_Architecture;
            }
        }

       
        static void MakeSureArchitecture()
        {
            if (m_Architecture == null)
            {
                if (Application.isPlaying)
                {
                    Application.quitting += CloseConnection;
                }
                m_Architecture = new T();
                m_Architecture.Init();
                SQLiteHelper();
            }
            OnRegisterPatch?.Invoke(m_Architecture);

            foreach (var item in m_Architecture.m_Models)
            {

               
                item.Init();

            }
            foreach (var item in m_Architecture.m_Sytems)
            {
                item.Init();
            }
            m_Architecture.m_Sytems.Clear();
            m_Architecture.m_Models.Clear();
            m_Architecture.m_Inited = true;

        }
        public  static void CloseConnection()
        {
            SQLite.CloseConnection();
        }

        public static void SQLiteHelper()
        {
#if !UNITY_ASSERTIONS ||UNITY_EDITOR
            SQLite = new SQLiteHelper(Application.streamingAssetsPath + "/" + "Interstellar.db");

#else
                    CopyDatabaseToLocality(Application.streamingAssetsPath);
                    SQLite = new SQLiteHelper(Application.persistentDataPath +"/" + "Interstellar.db");
#endif
            foreach (var item in m_Architecture.m_Models)
            {
                item.SQLit = SQLite;
            }
        }


       
        public static   void CopyDatabaseToLocality(string databaseName)
        {
            string Fillpath = "/" + databaseName + ".db";
            WWW www = new WWW(Application.streamingAssetsPath + Fillpath);

            Debug.Log("????????" + Application.persistentDataPath + Fillpath);
           
            while (!www.isDone)
            {
              
            }

            string path = Application.persistentDataPath + Fillpath;

            if (File.Exists(path))
            {
                File.Delete(path);

            }
            File.WriteAllBytes(path, www.bytes);


        }


        private IOCContainer m_Container = new IOCContainer();

        // ????????????????
        protected abstract void Init();

        // ?????????????????? API
        public void Register<T>(T instance) {
            MakeSureArchitecture();
            m_Architecture.m_Container.Register<T>(instance);
        }

        public void RegisterModel<T>(T model) where T : IModel {
            model.SetArchitecture(this);
            m_Container.Register<T>(model);

            if(!m_Inited) {
                m_Models.Add(model);
            } else {
                model.SQLit = SQLite;
                model.Init();
            }
        }
        public void RegisteUtility<T>(T utility) {
            m_Container.Register<T>(utility);
        }

        public void RegisterSystem<T>(T Sytem) where T : ISystem {
            Sytem.SetArchitecture(this);
            m_Container.Register<T>(Sytem);

            if(!m_Inited) {
                m_Sytems.Add(Sytem);
            } else {
                Sytem.Init();
            }
        }

        public T GetUtility<T>() where T : class {
            return m_Container.Get<T>();
        }

        public T GetModel<T>() where T : class, IModel {
            return m_Container.Get<T>();
        }

        public T GetSystem<T>() where T : class {
            return m_Container.Get<T>();
        }

        public void SendCommand<T>() where T : ICommand, new() {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand {
            command.SetArchitecture(this);
            command.Execute();
        }
        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem(); // +
        public void SendEvent<T>() where T : new() {
            mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e) {
            mTypeEventSystem.Send<T>(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent) {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent) {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }

    }

}

