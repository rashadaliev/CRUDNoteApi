using DataAccess;

namespace BusinessLogic;

public class NoteService(INoteRepository noteRepository) : INoteService
{
    public async Task CreateAsync(string text, string? image, CancellationToken cancellationToken = default)
    {
        var note = new Note
        {
            Title = text.Length > 50 ? text.Substring(0,50) : text,
            Text = text,
            ImagePath = image
        };

        await noteRepository.CreateAsync(note, cancellationToken);
    }

    public async Task CreateWithTitleAsync(string title, string text, string? image, CancellationToken cancellationToken = default)
    {
        var note = new Note
        {
            Title = title.Length > 50 ? title.Substring(0,50) : title,
            Text = text,
            ImagePath = image
        };

        await noteRepository.CreateAsync(note, cancellationToken);
    }

    public async Task<string> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await noteRepository.GetByIdAsync(id, cancellationToken);
        if (note is null)
            throw new Exception("Note not found!");

        return note.Text;
    }

    public async Task<IEnumerable<string>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notes = await noteRepository.GetAllAsync(cancellationToken);
        return notes
            .Where(note => !string.IsNullOrEmpty(note.Title))
            .Select(note => note.Title)!;
    }

    public async Task UpdateAsync(Guid id, string newText, string? image, CancellationToken cancellationToken = default)
    {
        var note = await noteRepository.GetByIdAsync(id, cancellationToken);
        if (note is null)
            throw new Exception("Note not found!");

        note.Text = newText;
        note.ImagePath = image;
        await noteRepository.UpdateAsync(note, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await noteRepository.GetByIdAsync(id, cancellationToken);
        if (note is null)
            throw new Exception("Note not found!");

        await noteRepository.DeleteAsync(note, cancellationToken);
    }
}