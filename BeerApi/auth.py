from http import client
import json
import requests

conn = client.HTTPSConnection("dev-ruojzgug.eu.auth0.com")

payload = "{\"client_id\":\"2kEAn3OOKSXRJ7meCk7cZneCNxqkbECB\",\"client_secret\":\"ik6XyR9Qwe4bSTYWr5SpxROVEwVVtAmpfdYY5vRJM9AclCeXFnxCIoaFbz6GWxRf\",\"audience\":\"https://beerapi:5001/api\",\"grant_type\":\"client_credentials\"}"

headers = { 'content-type': "application/json" }

conn.request("POST", "/oauth/token", payload, headers)

res = conn.getresponse()
data = res.read()

auth_token = json.loads(data.decode("utf-8"))

print(auth_token["token_type"])
print(auth_token["access_token"])
conn.close()

headers = { 'authorization':  auth_token["token_type"] + " " + auth_token["access_token"]}
res = requests.request(url="https://localhost:5001/api/beers", headers=headers, method='GET', verify=False)
print(res.content)
print(res.status_code)
input()