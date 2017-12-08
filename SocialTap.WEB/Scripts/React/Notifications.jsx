class CommentBox extends React.Component{
  render() {
    return (
        <div className="text-center">
            <div className="form-group">
                <h2>Choose the bar please right here</h2>
                <hr />
                <div className="col-md-6">
                    <input className="form-control"/>
                 </div>
              </div>
      </div>
    );
  }
}

ReactDOM.render(
  <CommentBox />,
  document.getElementById('content')
);
