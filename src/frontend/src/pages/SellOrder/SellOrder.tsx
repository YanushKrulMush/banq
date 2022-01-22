import { Button } from "@material-ui/core";
import { useState } from "react";
import SellOrderDialog from "./SellOrderDialog";

const SellOrder = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (

    <>
      <Button
        variant="outlined"
        color="primary"
        onClick={() => setIsOpen(true)}
      >
        Zlecenie sprzedaży
      </Button>
      <SellOrderDialog
        isOpen={isOpen}
        handleClose={() => setIsOpen(false)}
      />
    </>
  )
}

export default SellOrder;