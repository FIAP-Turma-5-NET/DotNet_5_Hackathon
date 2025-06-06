﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<int> CadastrarAsync(Usuario usuario);
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<Usuario?> ObterPorCpfAsync(string login);
        Task<Usuario?> ObterPorEmailAsync(string login);
        Task<Usuario?> ObterPorCrmAsync(string login);
        Task<bool> VerificarExistentePorCpfOuEmailAsync(string cpf, string email);
        Task<IEnumerable<Usuario>> ListarMedicosAsync(int? especialidadeId = null, string? nome = null, string? crm = null);
        Task InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds);     
    }
}
