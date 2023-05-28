using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;


public class MantleManager : MonoBehaviour
{
    public static MantleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    public async void TransferToken(int scoreToSend)
    {
        if(scoreToSend == 0)
        {
            return;
        }

        try
        {
            var privateKeyPublicKey = "0x88fd26752ee8f950c749ce4c053b32debf5c35beac3c1c5b90edc85e565f6a32";

            string unvalidString = PlayerPrefs.GetString("Wallet").ToString();

            string targetAddress = Regex.Replace(unvalidString, @"[^\x20-\x7F]", "");


            var account = new Nethereum.Web3.Accounts.Account(privateKeyPublicKey, new BigInteger(5001));


            Web3 web3 = new Web3(account, "https://rpc.testnet.mantle.xyz/");

            web3.TransactionManager.UseLegacyAsDefault = true;

            var mintHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var mint = new TransferFunction()
            {
                To = targetAddress,
                Score = new BigInteger(scoreToSend * 1000000000000000000),
            };

            var estimate = await mintHandler.EstimateGasAsync("0x16196d9e0F46E726F446Df6B926907B8DCc8726E", mint);
            mint.Gas = estimate.Value;

            var mintReceipt = await mintHandler.SendRequestAndWaitForReceiptAsync("0x16196d9e0F46E726F446Df6B926907B8DCc8726E", mint);
            Debug.Log($"Sent {scoreToSend} tokens by transaction: {mintReceipt.TransactionHash}");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

public class TransferFunction : TransferFunctionBase
{
}

[Function("transfer")]
public class TransferFunctionBase : FunctionMessage
{
    [Parameter("address", "_to", 1)]
    public virtual string To { get; set; }

    [Parameter("uint256", "_score", 2)]
    public virtual BigInteger Score { get; set; }
}
