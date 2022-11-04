using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp_console
{
    public class View : IView
    {
        public event Action UserInfoSelected;
        public event Action TrackListSelected;
        public event Action TrackUserListSelected;
        public event Action UserListSelected;
        public event Action PlaylistListSelected;
        public event Action<int> TrackInfoSelected;
        public event Action<int> PlaylistInfoSelected;
        public event Action<int> TrackDeleteFromUserSelected;
        public event Action<int, int> DeleteTrackFromPlaylistSelected;
        public event Action<int> TrackAddToUserSelected;
        public event Action<int, int> TrackAddToPlaylistSelected;
        public View() { }
        public void StartMenu()
        {
            string Menu = "\nВыберите нужный пункт меню:\n" +
                "0 - Выход\n" +
                "1 - Вывод информации о пользователе\n" +
                "2 - Вывод всех треков пользовательской медиатеки\n" +
                "3 - Вывод всех треков Библиотеки MIDI треков\n" +
                "4 - Вывод информации о треке\n" +
                "5 - Вывод всех плейлистов пользовательской медиатеки\n" +
                "6 - Вывод информации о плейлисте\n" +
                "7 - Вывод всех пользователей в системе\n" +
                "8 - Удалить трек из медиатеки\n" +
                "9 - Удалить трек из плейлиста\n" +
                "10 - Добавить трек в плейлист\n" +
                "11 - Добавить трек в медатеку\n";

            string Choice = "";

            while (Choice != "0")
            {
                switch (Choice)
                {
                    case "1":
                        this.UserInfoSelected();
                        break;

                    case "2":
                        this.TrackUserListSelected();
                        break;

                    case "3":
                        this.TrackListSelected();
                        break;

                    case "4":
                        Console.Write("Введите ID трека: ");
                        int tid = Convert.ToInt32(Console.ReadLine());
                        this.TrackInfoSelected(tid);
                        break;

                    case "5":
                        this.PlaylistListSelected();
                        break;

                    case "6":
                        Console.Write("Введите ID плейлиста: ");
                        int pid = Convert.ToInt32(Console.ReadLine());
                        this.PlaylistInfoSelected(pid);
                        break;

                    case "7":
                        this.UserListSelected();
                        break;

                    case "8":
                        Console.Write("Введите ID трека, который хотите удалить: ");
                        int tdid = Convert.ToInt32(Console.ReadLine());
                        this.TrackDeleteFromUserSelected(tdid);
                        break;

                    case "9":
                        Console.Write("Введите ID плейлиста, из которого хотите удалить трек: ");
                        int pdid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите ID трека, который хотите удалить: ");
                        tdid = Convert.ToInt32(Console.ReadLine());
                        this.DeleteTrackFromPlaylistSelected(tdid, pdid);
                        break;

                    case "10":
                        Console.Write("Введите ID плейлиста, в который хотите добавить трек: ");
                        pid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите ID трека, который хотите добавить: ");
                        tid = Convert.ToInt32(Console.ReadLine());
                        this.TrackAddToPlaylistSelected(pid, tid);
                        break;

                    case "11":
                        Console.Write("Введите ID трека, который хотите добавить в медиатеку: ");
                        tid = Convert.ToInt32(Console.ReadLine());
                        this.TrackAddToUserSelected(tid);
                        break;

                    default:
                        break;
                }

                Console.WriteLine(Menu);
                Console.Write("Выбор: ");
                Choice = Console.ReadLine();
                Console.WriteLine();
            }
        }
        public void SetUserInfo(User u)
        {
            string res = "Логин - " + u.Login + "; Имя - " + u.Name +
                         "; Почта - " + u.Mail + "; Дата рождения - " + 
                         u.DateOfBirth.Value.ToString() + "\n";

            Console.WriteLine(res);
        }
        public void SetTrackList(List<Track> tracks)
        {
            foreach (Track t in tracks)
            {
                Console.WriteLine("ID - " + t.Id + "; Название - " + t.Name);
            }

            Console.WriteLine();
        }
        public void SetTrackUserList(List<Track> tracks)
        {
            foreach (Track t in tracks)
            {
                Console.WriteLine("ID - " + t.Id + "; Название - " + t.Name);
            }

            Console.WriteLine();
        }
        public void SetUserList(List<User> users)
        {
            foreach (User u in users)
            {
                Console.WriteLine("ID - " + u.Id.ToString() + "; Логин - " + u.Login + "; Имя - " + u.Name +
                         "; Тип - " + u.UserType  + "\n");
            }

            Console.WriteLine();
        }
        public void SetPlaylistList(List<Playlist> playlists)
        {
            foreach (Playlist p in playlists)
            {
                Console.WriteLine("ID - " + p.Id + "; ID автора - " + p.AuthorId + "; Название - " + p.Name);
            }

            Console.WriteLine();
        }
        public void SetTrackInfo(Track t)
        {
            Console.WriteLine("\nID - " + t.Id + "; Название - " + t.Name + "; Дата создания - " + 
                t.DateOfCreation.ToString() + "; Описание - " + t.Description);
        }
        public void SetPlaylistInfo(Playlist p)
        {
            Console.WriteLine("\nID - " + p.Id + "; ID автора - " + p.AuthorId + "; Название - " + p.Name + 
                "; Описание - " + p.Description);

            Console.WriteLine("\nТреки из данного плейлиста: ");
            foreach (Track t in p.tracks)
            {
                Console.WriteLine("ID - " + t.Id + "; Название - " + t.Name);
            }

            Console.WriteLine();
        }
    }
}
