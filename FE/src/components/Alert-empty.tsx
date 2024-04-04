import "../styles/conversation.scss";
const AlertEmpty = (props: any) => {
  return (
    <>
      <div className={`conversation__wrapper ${props.classAdd}`}>
        <div className="conversation__alert-empty">
          <div className="conversation__alert-content">
            <h1>{props.message}</h1>
          </div>
        </div>
      </div>
    </>
  );
};

export default AlertEmpty;
