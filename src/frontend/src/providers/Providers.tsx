import { QueryClient, QueryClientProvider } from "react-query";
import {
  createMuiTheme,
  StylesProvider,
  ThemeProvider,
} from "@material-ui/core/styles";
import { ThemeProvider as StyledThemeProvider } from "styled-components";
import { ReactQueryDevtools } from "react-query/devtools";
import React from "react";
import { plPL } from "@material-ui/core/locale";
import { plPL as plPL2 } from "@material-ui/data-grid";
import { MuiPickersUtilsProvider } from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import { pl } from "date-fns/locale";
import { SnackbarProvider } from "notistack";
import ApiClientProvider from "./ApiClientProvider";
import AuthProvider from "./AuthProvider";

const queryClient = new QueryClient();

const theme = createMuiTheme(
  {
    spacing: (x) => `${x * 8}px`,
  },
  plPL,
  plPL2
);

type Props = {
  children: React.ReactNode;
};

const Providers = ({ children }: Props) => (
  <MuiPickersUtilsProvider utils={DateFnsUtils} locale={pl}>
    <StylesProvider injectFirst>
      <StyledThemeProvider theme={theme}>
        <ThemeProvider theme={theme}>
          <SnackbarProvider
            anchorOrigin={{ vertical: "top", horizontal: "center" }}
          >
            <QueryClientProvider client={queryClient}>
              <AuthProvider>
                <ApiClientProvider>
                  {children}
                  <ReactQueryDevtools initialIsOpen={false} />
                </ApiClientProvider>
              </AuthProvider>
            </QueryClientProvider>
          </SnackbarProvider>
        </ThemeProvider>
      </StyledThemeProvider>
    </StylesProvider>
  </MuiPickersUtilsProvider>
);

export default Providers;
