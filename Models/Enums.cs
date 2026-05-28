namespace NeuroVestAPI.Models
{
    public enum TipoUsuario
    {
        MEDICO,
        PACIENTE,
        ADMIN
    }

    public enum TipoDispositivo
    {
        VASCULAR,
        NEURAL
    }

    public enum ModoFuncionamento
    {
        SIMULACAO,
        REAL
    }

    public enum StatusGeral
    {
        NORMAL,
        ATENCAO,
        CRITICO
    }

    public enum CategoriaRecomendacao
    {
        URGENTE,
        IMPORTANTE,
        PREVENTIVO,
        ESTILO_DE_VIDA
    }

    public enum CriticidadeAlerta
    {
        INFO,
        ATENCAO,
        URGENTE
    }

    public enum StatusExportacao
    {
        SUCESSO,
        FALHA
    }

    public enum TipoIntervencao
    {
        RECALIBRACAO,
        TROCA_BATERIA,
        ATUALIZACAO_FIRMWARE
    }
}