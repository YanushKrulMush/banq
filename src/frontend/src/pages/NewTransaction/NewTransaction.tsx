import { Button } from "@material-ui/core";
import { useState } from "react";
import NewTransactionDialog from "./NewTransactionDialog";

const NewTransaction = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (

    <>
      <Button
        variant="outlined"
        color="primary"
        onClick={() => setIsOpen(true)}
      >
        Nowy przelew
      </Button>
      <NewTransactionDialog
        isOpen={isOpen}
        handleClose={() => setIsOpen(false)}
      />
    </>
  )
}

export default NewTransaction;