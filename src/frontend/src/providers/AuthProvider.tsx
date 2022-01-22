import React, { createContext, useContext, useEffect, useState } from "react";
import { useSnackbar } from "notistack";
import useSignInMutation from "../queries/useSignInMutation";

type Props = {
  children: React.ReactNode;
};

export enum Perform {
  occupyParkingSpot = "OccupyParkingSpot",
  bookParkingSpot = "BookParkingSpot",
  cancelBookedSpot = "CancelBookedSpot",
  releaseParkingSpot = "ReleaseParkingSpot",
}

export type User = {
  token: string;
  permissions: Perform[];
};

export type SignIn = {
  email: string, password: string
};

type AuthContext = {
  user?: User;
  can: (perform: Perform) => boolean;
  signIn: (fields: SignIn) => void;
  signOut: () => void;
};


const useProvideAuth = (): AuthContext => {
  const [user, setUser] = useState<AuthContext["user"] | undefined>();
  const signInMutation = useSignInMutation();
  const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    const auth = localStorage.getItem("auth");
    if (auth) {
      setUser(JSON.parse(auth));
    }
  }, []);

  const can = (perform: Perform) =>
    Boolean(user?.permissions.includes(perform));

  const signIn = (fields: SignIn) => {
    signInMutation.mutate(fields, {
      onSuccess: (response) => {
        setUser(response);
        localStorage.setItem("auth", JSON.stringify(response));
      },
    });
  };

  const signOut = () => {
    setUser(undefined);
    localStorage.removeItem("auth");
    enqueueSnackbar("Wylogowano z systemu", { variant: "info" });
  };

  return {
    user,
    can,
    signIn: signIn,
    signOut,
  };
};

const authContext = createContext<AuthContext>({
  can: (perform: Perform): boolean => false,
  signIn(fields: SignIn): void {},
  signOut(): void {},
});

export const useAuth = () => useContext(authContext);

const AuthProvider = ({ children }: Props) => {
  const auth = useProvideAuth();
  return <authContext.Provider value={auth}>{children}</authContext.Provider>;
};

export default AuthProvider;
