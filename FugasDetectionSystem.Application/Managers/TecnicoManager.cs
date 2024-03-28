using FugasDetectionSystem.Common.Models;
using FugasDetectionSystem.Domain.Entities;
using FugasDetectionSystem.Domain.Interfaces;


namespace FugasDetectionSystem.Application.Managers
{
    public class TecnicoManager
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public TecnicoManager(ITecnicoRepository tecnicoRepository)
        {
            _tecnicoRepository = tecnicoRepository;
        }

        public async Task<Result<List<Tecnico>>> GetAllTecnicosAsync()
        {
            try
            {
                var tecnicos = await _tecnicoRepository.GetAllAsync();
                return Result<List<Tecnico>>.Success(tecnicos.ToList());
            }
            catch (Exception ex)
            {
                return Result<List<Tecnico>>.Failure(ex.Message);
            }
        }


    }
}
