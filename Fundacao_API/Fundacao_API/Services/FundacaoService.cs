using Fundacao_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Fundacao_API.Services
{
    public class FundacaoService
    {
        public IRepository _repo { get; }
        public FundacaoService(IRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<Fundacao>> GetAllFundacoes()
        {
            var result = await _repo.GetAllFundacoesAsync();
            return result;
        }


        public async Task<Fundacao> GetFundacao(int IDFundacao)
        {
            var result = await _repo.GetFundacaoAsyncByCNPJ(IDFundacao);
            return result;
        }

        public async Task<bool> AdicionarFundacao(Fundacao Fundacao)
        {
            if (isCNPJ(Fundacao.CNPJ))
            {
                if (await verificaSeExisteCNPJ(Fundacao.CNPJ))
                {
                    try
                    {
                        _repo.Add(Fundacao);
                        if (await _repo.SaveChangesAsync())
                        {
                            return true;
                        }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            
            return false;
                 
        }

        public async Task<bool> DeletarFundacao(int IDFundacao)
        {
            try
            {
                var fundacao = await _repo.GetFundacaoAsyncByCNPJ(IDFundacao);

                if (fundacao == null)
                {
                    return false;
                }

                _repo.Delete(fundacao);

                if (await _repo.SaveChangesAsync())
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return false;
           
        }

        public async Task<bool> AlterarFundacao(Fundacao Fundacao)
        {
            try
            {               
                if (Fundacao == null)
                {
                    return false;
                }

                if (isCNPJ(Fundacao.CNPJ))
                {
                    _repo.Update(Fundacao);

                    if (await _repo.SaveChangesAsync())
                    {
                        return true;
                    }
                }

                return false;               
            }
            catch (System.Exception)
            {
                return false;
            }
  
        }



        private async Task<bool> verificaSeExisteCNPJ(string CNPJ)
        {
            var listaFundacao = await GetAllFundacoes();
            foreach (var fundacao in listaFundacao)
            { 
                if(fundacao.CNPJ == CNPJ)
                {
                    return false;
                }
            }
            return true;            
        }


        private bool isCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            
            if (cnpj.Length != 14)
                return false;
            
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            
            return cnpj.EndsWith(digito);
        }


    }
    
}
