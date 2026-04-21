using JardimSimplesApp.Models;
using System.Collections.ObjectModel;

namespace JardimSimplesApp.Services
{
    public static class ServicoRepository
    {
        //Criando o objeto da lista de serviços, usando ObservableCollection para notificar a UI sobre mudanças.
        public static ObservableCollection<ServicoJardinagem> Servicos { get; } = new ObservableCollection<ServicoJardinagem>(); 

        private static int _proximoId = 1;

        public static void CarregarDadosIniciais()
        {
            // >>> IMPORTANTE: evita duplicar os dados quando o ViewModel é recriado
            if (Servicos.Count > 0)
                return;

            // Dados fake para criar a lista inicial de serviços. Na prática, isso viria de um banco de dados ou API.
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

        // Métodos para manipular a coleção de serviços.
        // Na prática, esses métodos poderiam interagir com um banco de dados ou API.
        public static void Adicionar(ServicoJardinagem servico)
        {
            servico.Id = _proximoId++;
            Servicos.Add(servico);
        }

        // Atualiza um serviço existente na coleção. Isso é importante para garantir
        // que a CollectionView seja notificada da mudança.
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

        // Remove um serviço da coleção por ID. Isso é útil para garantir
        // que a CollectionView seja notificada da remoção.
        public static void RemoverPorId(int id)
        {
            var servico = Servicos.FirstOrDefault(s => s.Id == id);
            if (servico != null)
                Servicos.Remove(servico);
        }

        // Remove um serviço da coleção por referência. Isso é útil para garantir
        public static void Remover(ServicoJardinagem servico)
        {
            if (Servicos.Contains(servico))
                Servicos.Remove(servico);
        }
    }
}



