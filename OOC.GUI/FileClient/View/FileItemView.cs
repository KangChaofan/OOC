using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.ServiceModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FileClient.Annotations;
using FileClient.FileService;

namespace FileClient.View
{
    public class FileItemView : INotifyPropertyChanged
    {
        private readonly FileServiceClient Client = new FileServiceClient();
        //        private static readonly Logger _logger = new Logger("OOC.GUI.FileClient.log");
        private DateTime _accessTime;
        private DateTime _createTime;
        private ImageSource _icon;
        private bool _isDirectory;
        private DateTime _modifyTime;
        private string _name;
        private long _size;
        private ObservableCollection<FileItemView> _subItems;
        private string _displayName;

        public bool IsDirectory
        {
            get { return _isDirectory; }
            set
            {
                _isDirectory = value;
                OnPropertyChanged("IsDirectory");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public long Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }

        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                OnPropertyChanged("CreateTime");
            }
        }

        public DateTime AccessTime
        {
            get { return _accessTime; }
            set
            {
                _accessTime = value;
                OnPropertyChanged("AccessTime");
            }
        }

        public DateTime ModifyTime
        {
            get { return _modifyTime; }
            set
            {
                _modifyTime = value;
                OnPropertyChanged("ModifyTime");
            }
        }

        public ObservableCollection<FileItemView> SubItems
        {
            get
            {
                //hack
                //                return new ObservableCollection<FileItemView>
                //                    {
                //                        new FileItemView {Name = "test1"},
                //                        new FileItemView(){Name = "test2"}
                //                    };

                try
                {
                    _subItems = getSubItems(Name);
                }
                catch (FaultException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return _subItems;
            }
            set { _subItems = value; }
        }

        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public string DisplayName
        {
            get
            {
                if (Name.Equals(string.Empty))
                {
                    return "Root";
                }
                return Path.GetFileName(Name);
            }
            set
            {
                _displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FileItemView> getSubItems(string path)
        {
            ObservableCollection<FileItemView> result = new ObservableCollection<FileItemView>();
            FileSystemDescription fileSystemDescription = Client.Stat(path);
            if (fileSystemDescription.IsDirectory)
            {
                FileSystemDescription[] fileDescriptions = Client.List(path);
                foreach (FileSystemDescription file in fileDescriptions)
                {
                    if (file.IsDirectory)
                    {
                        result.Add(new FileItemView
                           {
                               Name = file.Name,
                               Size = -1,
                               CreateTime = file.CreateTime,
                               AccessTime = file.AccessTime,
                               ModifyTime = file.ModifyTime,
                               IsDirectory = true,
                               Icon = new BitmapImage(new Uri(@"Resources/Images/Folder16.png",
                                                                    UriKind.RelativeOrAbsolute)),
                               SubItems = getSubItems(file.Name),
                           });
                    }
                    else
                    {
                        result.Add(new FileItemView
                        {
                            Name = file.Name,
                            Size = file.Size,
                            CreateTime = file.CreateTime,
                            AccessTime = file.AccessTime,
                            ModifyTime = file.ModifyTime,
                            IsDirectory = true,
                            Icon = new BitmapImage(new Uri(@"Resources/Images/File16.png",
                                                                 UriKind.RelativeOrAbsolute)),
                        });
                    }

                }
            }
            else
            {
                Console.WriteLine(@"haha");
            }
            return result;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}