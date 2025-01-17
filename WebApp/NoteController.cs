using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebAppi.DTO;

namespace WebAppi;

[ApiController]
[Route("Note")]
public class NoteController(INoteService noteService) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(CreateNoteWithoutTitleDto createNoteWithoutTitleDto)
    {
        await noteService.CreateAsync(createNoteWithoutTitleDto.Text, createNoteWithoutTitleDto.Image);
        return NoContent();
    }

    [HttpPost("CreateWithTitle")]
    public async Task<IActionResult> CreateWithTitle(CreateNoteWithTitleDto createNoteWithTitleDto)
    {
        await noteService.CreateWithTitleAsync(createNoteWithTitleDto.Title, 
            createNoteWithTitleDto.Text, createNoteWithTitleDto.Image);
        return NoContent();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetNoteAsync([FromRoute]Guid id)
    {
        var result = await noteService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("GetAllNotes")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notes = await noteService.GetAllAsync(cancellationToken);
        if (!notes.Any())
            return NotFound("Нету");
        return Ok(notes);
    }
    
    [HttpPut("UpdateNote")]
    public async Task<IActionResult> UpdateNoteAsync([FromBody]UpdateNoteDto updateNoteDto)
    {
        await noteService.UpdateAsync(updateNoteDto.Id, updateNoteDto.NewText, updateNoteDto.Image);
        return NoContent();
    }
    
    [HttpDelete("DeleteNote")]
    public async Task<IActionResult> DeleteNoteAsync([FromBody]DeleteNoteDto deleteNoteDto)
    {
        await noteService.DeleteAsync(deleteNoteDto.Id);
        return NoContent();
    }
}