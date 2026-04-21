using JardimSimplesApp.Models;
using System.Collections.ObjectModel;

namespace JardimSimplesApp.Services
{
    public static class ServicoRepository
    {
        //Criando o objeto da lista de serviços, usando ObservableCollection para notificar a UI sobre mudanças.
        public static ObservableCollection<ServicoJardinagem> Servicos { get; private set; }
            = new ObservableCollection<ServicoJardinagem>();

        private static int _proximoId = 1;

        public static void CarregarDadosIniciais()
        {
            if (Servicos.Count > 0)
                return;

            Servicos.Add(new ServicoJardinagem
            {
                Id = _proximoId++,
                Cliente = "Maria Oliveira",
                TipoServico = "Corte de grama",
                Endereco = "Rua das Flores, 120",
                DataServico = DateTime.Today.AddDays(1),
                Valor = 120.00m,
                Status = "Agendado"
            });

            Servicos.Add(new ServicoJardinagem
            {
                Id = _proximoId++,
                Cliente = "Carlos Santos",
                TipoServico = "Poda de arbustos",
                Endereco = "Av. Verde Vida, 85",
                DataServico = DateTime.Today.AddDays(2),
                Valor = 180.00m,
                Status = "Agendado"
            });

            Servicos.Add(new ServicoJardinagem
            {
                Id = _proximoId++,
                Cliente = "Ana Beatriz",
                TipoServico = "Limpeza de jardim",
                Endereco = "Travessa Primavera, 42",
                DataServico = DateTime.Today,
                Valor = 150.00m,
                Status = "Concluído"
            });
        }

        public static void Adicionar(ServicoJardinagem servico)
        {
            servico.Id = _proximoId++;
            Servicos.Add(servico);
        }

        public static void Atualizar(ServicoJardinagem servicoAtualizado)
        {
            var indice = Servicos.ToList().FindIndex(s => s.Id == servicoAtualizado.Id);
            if (indice >= 0)
            {
                // Substitui o item inteiro na coleção.
                // Isso força a atualização visual da CollectionView.
                Servicos[indice] = servicoAtualizado;
            }
        }

        public static void RemoverPorId(int id)
        {
            var servico = Servicos.FirstOrDefault(s => s.Id == id);
            if (servico != null)
                Servicos.Remove(servico);
        }

        public static void Remover(ServicoJardinagem servico)
        {
            if (Servicos.Contains(servico))
                Servicos.Remove(servico);
        }
    }
}



