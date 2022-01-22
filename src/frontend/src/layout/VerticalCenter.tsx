import styled from "styled-components";

type Props = {
  height: string;
  topMargin?: string;
  bottomMargin?: string;
};

const VerticalCenter = styled.div<Props>`
  display: flex;
  flex-direction: column;
  height: ${({ height }) => height};
  :before {
    min-height: ${({ topMargin }) => topMargin ?? "0px"};
    content: "";
    display: block;
    flex-grow: 1;
  }
  :after {
    min-height: ${({ bottomMargin }) => bottomMargin ?? "0px"};
    content: "";
    display: block;
    flex-grow: 1;
  }
`;

export default VerticalCenter;
