class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">
                <h1>{this.props.title}</h1>
                <Part />
            </div>
        );
    }
}