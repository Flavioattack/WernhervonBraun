using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIoTD.Data;
using CIoTD.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIoTD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Device
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            var devices = await _context.Devices.ToListAsync();

            // Para cada dispositivo, buscar os comandos associados
            foreach (var device in devices)
            {
                var commands = await _context.CommandDescriptions
                    .Where(cd => cd.DeviceId == device.Identifier)
                    .ToListAsync();

                device.Commands = commands; // Define os comandos no dispositivo
            }

            return devices;
        }

        // GET: api/Device/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(string id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            // Busca os comandos relacionados ao dispositivo
            var commands = await _context.CommandDescriptions
                .Where(cd => cd.DeviceId == id)
                .ToListAsync();

            device.Commands = commands; // Define os comandos no dispositivo

            return device;
        }

        // POST: api/Device
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = device.Identifier }, device);
        }

        // PUT: api/Device/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(string id, Device device)
        {
            if (id != device.Identifier)
            {
                return BadRequest();
            }

            var existingDevice = await _context.Devices
                .Include(d => d.Commands) // Inclui os comandos associados ao dispositivo
                .FirstOrDefaultAsync(d => d.Identifier == id);

            if (existingDevice == null)
            {
                return NotFound();
            }
            //recupera os comandos do dispositivo
            var commands = await _context.CommandDescriptions
                    .Where(cd => cd.DeviceId == existingDevice.Identifier)
                    .ToListAsync();

            // Remove os comandos existentes
            _context.CommandDescriptions.RemoveRange(commands);

            // Adiciona os novos comandos
            if (device.Commands != null && device.Commands.Any())
            {

                foreach (var command in device.Commands)
                {
                    // Garante que o DeviceId está definido corretamente
                    command.DeviceId = id;
                    // Correção caso seja enviado um Id que é (Identity)
                    command.Id = 0;
                    _context.CommandDescriptions.Add(command);
                }
            }

            // Atualiza as outras propriedades do dispositivo
            existingDevice.Description = device.Description;
            existingDevice.Manufacturer = device.Manufacturer;
            existingDevice.Url = device.Url;

            // Define o estado do dispositivo como modificado
            _context.Entry(existingDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // DELETE: api/Device/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(string id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(string id)
        {
            return _context.Devices.Any(e => e.Identifier == id);
        }
    }
}
