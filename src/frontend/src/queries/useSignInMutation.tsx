import { useMutation, UseMutationOptions } from "react-query";
import { useSnackbar } from "notistack";
import { client } from "../providers/ApiClientProvider";
import { SignIn, User } from "../providers/AuthProvider";
import { CLIENT_SECRET } from "../env";

const AUTH_URL =
  "http://10.110.233.24:8080/auth/realms/master/protocol/openid-connect/token";

const authConfig = {
  headers: {
    "Content-Type": "application/x-www-form-urlencoded",
  },
};

const resolveParams = (email: string, password: string) =>
  new URLSearchParams({
    client_id: "bank",
    grant_type: "password",
    client_secret: CLIENT_SECRET,
    scope: "openid",
    username: email,
    password,
  });

export default function useSignInMutation(
  options?: UseMutationOptions<User, unknown, SignIn>
) {
  const { enqueueSnackbar } = useSnackbar();
  const apiClient = client;

  return useMutation<User, unknown, SignIn>(
    async ({ email, password }) => {
      const { data } = await apiClient.post(
        AUTH_URL,
        resolveParams(email, password),
        authConfig
      );
      return {
        token: data.access_token,
        permissions: [],
      };
    },
    {
      onError: (error) => {
        // @ts-ignore
        switch (error.response.status) {
          case 401:
            enqueueSnackbar("Wprowadzono niepoprawny email bądź hasło", {
              variant: "error",
            });
            break;
          default:
            enqueueSnackbar("Coś poszło nie tak, spróbuj ponownie", {
              variant: "error",
            });
            break;
        }
      },
      ...options,
    }
  );
}
