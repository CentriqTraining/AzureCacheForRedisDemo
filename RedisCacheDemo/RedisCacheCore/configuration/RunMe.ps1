# Promp for user login
az login 

# Create a resource group called CentriqAzureDemo
#   We'll use this only for our demo so we can easily clean up
az group create -n CentriqAzureDemo -l eastus

#  Create the Redis Cache (About $16.50 per month)
az redis create -n CentriqRedisDemo -g CentriqAzureDemo -l eastus --sku Basic --vm-size c0

# Query the Keys for this new Redis CAche and place it in a json file under our
#  configuration directory to be used when we run 
az redis list-keys -n CentriqRedisDemo -g CentriqAzureDemo > Config.json

Write-Host "==============================================================="
Write-Host "Ready to use your Redis cache..."
Write-Host "You can now set RedisCacheCore as your start up project and run"