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

# nginx
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo update
helm install nginx ingress-nginx/ingress-nginx -f ./dapr-annotations.yaml

kubectl apply -f .\ingress.yaml

kubectl apply -f dotnet.yaml

