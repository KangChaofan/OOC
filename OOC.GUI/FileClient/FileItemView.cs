using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FileClient.Annotations;
using FileClient.FileService;
using OOC.Util;

namespace FileClient
{
    public class FileItemView : INotifyPropertyChanged
    {
        private readonly FileServiceClient Client = new FileServiceClient();
        private readonly Logger _logger = new Logger("OOC.GUI.FileClient.log");
        private DateTime _accessTime;
        private DateTime _createTime;
        private ImageSource _icon;
        private bool _isDirectory;
        private DateTime _modifyTime;
        private string _name;
        private long _size;
        private ObservableCollection<FileItemView> _subItems;

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

                ObservableCollection<FileItemView> result = new ObservableCollection<FileItemView>();
                string path = Name;
                try
                {
                    _logger.Debug(string.Format("[STAT]{0}", path));
                    FileSystemDescription fileSystemDescription = Client.Stat(path);
                    if (fileSystemDescription.IsDirectory)
                    {
                        _logger.Debug(string.Format("[LIST]{0}", path));
                        FileSystemDescription[] fileDescriptions = Client.List(path);
                        foreach (FileSystemDescription file in fileDescriptions)
                        {
                            result.Add(new FileItemView
                                {
                                    Name = file.Name,
                                    Size = -1,
                                    CreateTime = file.CreateTime,
                                    AccessTime = file.AccessTime,
                                    ModifyTime = file.ModifyTime,
                                    IsDirectory = true,
                                    Icon = file.IsDirectory?new BitmapImage(new Uri("Resources/Images/Folder16.png")):new BitmapImage(new Uri("Resources/Images/Documents16.png")),
                                });
                        }
                    }
                }
                catch (FaultException ex)
                {
                    _logger.Warn(ex.Message);
                }
                return _subItems;
            }
            set
            {
                _subItems = value;
                OnPropertyChanged("SubItems");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}