namespace Notes.Models;

internal class Note
{
    public static Note Load(string filename)
    {
        filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename)) {
            throw new FileNotFoundException("Unable to find file on local storage.", filename);
        }

        return new Note
        {
            Filename = Path.GetFileName(filename),
            Text = File.ReadAllText(filename),
            Date = File.GetCreationTime(filename),
        };
    }

    public static IEnumerable<Note> LoadAll()
    {
        var appDataPath = FileSystem.AppDataDirectory;
        return Directory.EnumerateFiles(appDataPath, "*.notes.txt")
                        .Select(filename => Note.Load(Path.GetFileName(filename)))
                        .OrderByDescending(note => note.Date);
    }

    public Note()
    {
        Filename = $"{Path.GetRandomFileName()}.notes.txt";
        Text = String.Empty;
        Date = DateTime.Now;
    }

    public string Filename { get; set; }
    public string Text { get; set; }    
    public DateTime Date { get; set; }

    public void Save() =>
            File.WriteAllText(FilePath(), Text);

    public void Delete() 
        => File.Delete(FilePath()); 

    public string FilePath() 
        => System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename);
}