import React, { createContext, useContext, useMemo } from "react";
import axios, { AxiosInstance } from "axios";
import { useAuth } from "./AuthProvider";
import { useSnackbar } from "notistack";

export const ApiClientContext = createContext<AxiosInstance>(axios);

export function useApiClient() {
  return useContext(ApiClientContext);
}

export const client = axios.create({
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
  // baseURL: "https:localhost:5001",
  baseURL: "http://10.99.43.252/v1.0/invoke",
});

let requestInterceptor: number;
let responseInterceptor: number;

const ApiClientProvider = ({ children }: React.PropsWithChildren<unknown>) => {
  const auth = useAuth();
  const { enqueueSnackbar } = useSnackbar();

  const apiClient = useMemo(() => {
    client.interceptors.request.eject((requestInterceptor));
    client.interceptors.response.eject((responseInterceptor));

    requestInterceptor = client.interceptors.request.use((config) => {
      const configParam = config;

      const token = auth.user?.token;

      if (token) {
        // TODO: enable
        // configParam.headers.Authorization = `Bearer ${token}`;
      }

      return configParam;
    });

    responseInterceptor = client.interceptors.response.use(
      (response) => response,
      (error) => {
        switch (error.response.status) {
          case 400:
            enqueueSnackbar(
              error.response.data?.Message ??
                "Coś poszło nie tak, spróbuj ponownie później",
              { variant: "error" }
            );
            break;

          case 401:
            if (auth.user) {
              auth.signOut();
            }
            break;
        }

        return Promise.reject(error);
      }
    );
    return client;
  }, [auth, enqueueSnackbar]);

  return (
    <ApiClientContext.Provider value={apiClient}>
      {children}
    </ApiClientContext.Provider>
  );
};

export default ApiClientProvider;
