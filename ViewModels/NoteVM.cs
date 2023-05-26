using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class NoteVM : ObservableObject, IQueryAttributable
{
    private Models.Note note_;

    public string Text
    {
        get => note_.Text;
        set
        {
            if (note_.Text != value)
            {
                note_.Text = value;
                OnPropertyChanged();
            }
        }
    }
    public DateTime Date => note_.Date;
    public string Identifier => note_.Filename;

    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public NoteVM()
    {
        note_ = new Models.Note();
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public NoteVM(Models.Note note)
    {
        note_ = note;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        note_.Date = DateTime.Now;
        note_.Save();
        await Shell.Current.GoToAsync($"..?saved={note_.Filename}");
    }

    private async Task Delete()
    {
        note_.Delete();
        await Shell.Current.GoToAsync($"..?deleted={note_.Filename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        throw new NotImplementedException();
    }
}