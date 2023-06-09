using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private static List<Form> forms = new List<Form>();

        public FormController()
        {
            if (forms.Count > 0)
                return; // Evitar agregar formularios si ya existen

            forms.Add(new Form { Id = 1, Code = "123", Name = "Form 1", Description = "Description 1" });
            forms.Add(new Form { Id = 2, Code = "456", Name = "Form 2", Description = "Description 2" });
            forms.Add(new Form { Id = 3, Code = "789", Name = "Form 3", Description = "Description 3" });
            forms.Add(new Form { Id = 4, Code = "101", Name = "Form 4", Description = "Description 4" });
            forms.Add(new Form { Id = 5, Code = "202", Name = "Form 5", Description = "Description 5" });
        }

        [HttpGet]
        public IEnumerable<Form> Get()
        {
            return forms;
        }

        [HttpGet("{id}")]
        public ActionResult<Form> GetById(int id)
        {
            var form = forms.Find(f => f.Id == id);
            if (form == null)
                return NotFound();

            return form;
        }

        [HttpPost]
        public ActionResult<Form> CreateForm([FromForm] FormData formData)
        {
            var form = new Form
            {
                Id = GetNextId(),
                Code = formData.Code,
                Name = formData.Name,
                Description = formData.Description
            };

            forms.Add(form);
            return CreatedAtAction(nameof(GetById), new { id = form.Id }, form);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateForm(int id, [FromForm] FormData formData)
        {
            var form = forms.Find(f => f.Id == id);
            if (form == null)
                return NotFound();

            form.Code = formData.Code;
            form.Name = formData.Name;
            form.Description = formData.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteForm(int id)
        {
            var form = forms.Find(f => f.Id == id);
            if (form == null)
                return NotFound();

            forms.Remove(form);
            return NoContent();
        }

        private int GetNextId()
        {
            if (forms.Count == 0)
                return 1;

            return forms[forms.Count - 1].Id + 1;
        }
    }

    public class Form
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FormData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
