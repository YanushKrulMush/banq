cd ./dapr

dapr init -k

# zipkin
kubectl create deployment zipkin --image openzipkin/zipkin
kubectl expose deployment zipkin --type ClusterIP --port 9411
kubectl apply -f tracing.yaml

# redis
helm repo add bitnami https://charts.bitnami.com/bitnami
helm install redis bitnami/redis
kubectl apply -f pubsub.yaml
kubectl apply -f statestore.yaml

# ingress/nginx
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm install nginx-ingress ingress-nginx/ingress-nginx -f dapr-annotations.yaml
kubectl create -f .\ingress-routes.yaml

# keycloak
kubectl create -f https://raw.githubusercontent.com/keycloak/keycloak-quickstarts/latest/kubernetes-examples/keycloak.yaml
minikube service --url keycloak

# dotnet
minikube docker-env | Invoke-Expression
docker build .\src\backend\Internal -t dotnet-app:latest