﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Colaborador
{
    public class Patrocinador
    {
        public long IdPatrocinador { get; set; }
        public string NomeOuRazaoSocial { get; set; }
        public bool IsPj { get; set; }
        public long CpfCnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Mensagem { get; set; }
        public string MensagemResumo
        {
            get
            {
                int lenght = Mensagem?.Length ?? 0;
                if (lenght > 14)
                {
                    return Mensagem?.Substring(0, 14) ?? "";
                }
                return Mensagem ?? "";
            }
        }

        public string NomeRazaoSocialResumo
        {
            get
            {
                int lenght = NomeOuRazaoSocial?.Length ?? 0;
                if (lenght > 18)
                {
                    return NomeOuRazaoSocial?.Substring(0, 18) + "..." ?? "";
                }
                return NomeOuRazaoSocial ?? "";
            }
        }

        public string LogoTipoBase64 { get; set; }
        public string Observacoes { get; set; }
        public StatusPatrocinador Status { get; set; }
        public string LinkSite { get; set; }
        public string LinkInstagram { get; set; }
        public string LinkLinkedin { get; set; }
        public int OrdemApresentacao { get; set; }
    }

    public enum StatusPatrocinador : int
    {
        Pendente = 0,
        Ativo = 1,
        Bloqueado = 2
    }
}