using System.Collections.ObjectModel;
using Wp7Recipe_10_2_MVVM.Model;

namespace Wp7Recipe_10_2_MVVM.Repositories
{
    public static class NewsRepository
    {
        private static ObservableCollection<News> _news;
        public static ObservableCollection<News> GetNews()
        {
            return _news ?? (_news = new ObservableCollection<News>
                                         {
                                             new News
                                                 {
                                                     Id = 1,
                                                     Author = "Rana Jawad",
                                                     Body = "Libya's ex-leader Col Muammar Gaddafi has been killed after an assault on his birthplace of Sirte, officials say.The circumstances of his death are not yet clear. Video has emerged purporting to show Col Gaddafi being captured alive and bundled on to a truck.",
                                                     Title = "Muammar Gaddafi killed"
                                                 },
                                             new News
                                                 {
                                                     Id = 2,
                                                     Author = "Dayo Yusuf",
                                                     Body = "Kenya has announced that it will launch a major security operation in its capital to flush out sympathisers of the Somali Islamist group, al-Shabab.",
                                                     Title = "Kenya to target Nairobi al-Shabab"
                                                 },
                                             new News
                                                 {
                                                     Id = 3,
                                                     Author = "Lizo Mzimba",
                                                     Body = "George Clooney, whose new film The Ides of March has had its UK premiere, has said he has no plans to enter politics. However, the Hollywood star said he would continue to be involved on the sidelines. ",
                                                     Title = "Clooney: 'I won't run for office'"
                                                 }
                                         });
        }
    }
}