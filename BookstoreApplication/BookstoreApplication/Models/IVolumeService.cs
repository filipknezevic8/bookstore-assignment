using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IVolumeService
    {
        Task<List<VolumeDTO>> SearchVolumesByName(string filter);
    }
}
